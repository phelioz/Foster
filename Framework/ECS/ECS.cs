using System.Collections.Generic;
using System.Collections.Specialized;

namespace Foster.Framework
{
    public class ECS
    {
        State state = new();
        readonly OrderedDictionary systems = new();
        readonly Dictionary<View, List<ISystem>> views = new();

        private float fixedUpdateAccumulator = 0.0f;
        public float FixedUpdateFPS = 60.0f;
        public float FixedUpdateDelta => 1.0f / FixedUpdateFPS;
        public int FixedUpdateMaxIterations = 4;
        public float FixedUpdateAccumulatorRatio => fixedUpdateAccumulator / FixedUpdateDelta;

        public ECS()
        {
            SetState(state);
        }

        public ECS(State state)
        {
            SetState(state);
        }

        public void SetState(State state)
        {
            if (this.state != null)
            {
                foreach (var entity in state.entities.Values)
                {
                    RemoveFromViews(entity);
                }
                this.state.Engine = null;
            }

            this.state = state;
            this.state.Engine = this;

            foreach (var entity in state.entities.Values)
            {
                UpdateViews(entity);
            }
        }

        public State GetState()
        {
            return state;
        }

        public void Update(float deltaTime)
        {
            UpdateQueued();
            UpdateStates();
            UpdateTick(deltaTime);
        }

        private void UpdateQueued()
        {
            foreach (var keyValue in views)
            {
                var view = keyValue.Key;
                view.ApplyQueued();
            }
        }

        private void UpdateStates()
        {
            foreach (ISystem system in systems.Values)
            {
                foreach (var keyValue in views)
                {
                    var view = keyValue.Key;
                    var viewSystems = keyValue.Value;
                    if (viewSystems.Contains(system))
                    {
                        if (view.AddedEntities.Count != 0)
                        {
                            system.OnAdd(view, view.AddedEntities);
                            system.OnRemove(view, view.RemovedEntities);
                        }
                    }
                }
            }
            foreach (var keyValue in views)
            {
                var view = keyValue.Key;
                view.ClearStates();
            }

        }

        private void UpdateTick(float deltaTime)
        {
            // Update fixed tick calculation
            int updateFixedTicks = 0;
            fixedUpdateAccumulator += deltaTime;
            while (fixedUpdateAccumulator >= FixedUpdateDelta)
            {
                fixedUpdateAccumulator -= FixedUpdateDelta;
                updateFixedTicks++;
            }

            // Stop spiral of death
            if (updateFixedTicks > FixedUpdateMaxIterations)
            {
                updateFixedTicks = FixedUpdateMaxIterations;
            }

            foreach (ISystem system in systems.Values)
            {
                foreach (var keyValue in views)
                {
                    var view = keyValue.Key;
                    var viewSystems = keyValue.Value;
                    if (viewSystems.Contains(system))
                    {
                        for (int i = 0; i < updateFixedTicks; i++)
                        {
                            system.OnUpdateFixed(view, view.TrackedEntities, FixedUpdateDelta);
                        }
                        system.OnUpdate(view, view.TrackedEntities, deltaTime);
                    }
                }
            }
        }

        public ECS AddSystem(ISystem system)
        {
            var systemType = system.GetType();

            // Remove existing from views
            if (systems.Contains(systemType))
            {
                var existingSystem = systems[systemType] as ISystem;
                foreach (View view in existingSystem.GetViews())
                {
                    if (views.TryGetValue(view, out List<ISystem>? viewSystems))
                    {
                        viewSystems.Remove(existingSystem);
                    }
                }
            }

            systems[systemType] = system;
            foreach (View view in system.GetViews())
            {
                if (!views.TryGetValue(view, out List<ISystem>? viewSystems))
                {
                    viewSystems = new List<ISystem>();
                    views.Add(view, viewSystems);
                }
                viewSystems.Add(system);
            }

            if (state != null)
            {
                system.OnBegin();
            }

            return this;
        }

        public void AddEntity(Entity entity)
        {
            if (state == null || entity == null)
            {
                return;
            }

            state.entities.Add(entity.ID, entity);
            UpdateViews(entity);
        }

        public bool RemoveEntity(Entity entity)
        {
            if (state == null || entity == null)
            {
                return false;
            }

            RemoveFromViews(entity);
            return state.entities.Remove(entity.ID);
        }

        public bool RemoveEntity(string ID)
        {
            if (state == null)
            {
                return false;
            }

            var entity = state.entities[ID];
            return RemoveEntity(entity);
        }

        public void UpdateViews(Entity entity)
        {
            foreach (var view in views.Keys)
            {
                if (view.IsInView(entity))
                {
                    view.Add(entity);
                }
                else
                {
                    view.Remove(entity);
                }
            }
        }

        public void RemoveFromViews(Entity entity)
        {
            foreach (var view in views.Keys)
            {
                view.Remove(entity);
            }
        }
    }
}
