using System;
using System.Collections.Generic;

namespace Foster.Framework
{
    public class View
    {
        readonly ISet<Type> withComponents = new HashSet<Type>();
        readonly ISet<Type> withoutComponents = new HashSet<Type>();

        public ISet<Entity> TrackedEntities { get; private set; } = new HashSet<Entity>();
        public ISet<Entity> AddedEntities { get; private set; } = new HashSet<Entity>();
        public ISet<Entity> RemovedEntities { get; private set; } = new HashSet<Entity>();
        public ISet<Entity> ChangedEntities { get; private set; } = new HashSet<Entity>();

        public ISet<Entity> ToAddEntities { get; private set; } = new HashSet<Entity>();
        public ISet<Entity> ToRemoveEntities { get; private set; } = new HashSet<Entity>();

        public View With<T>() where T : IComponent
        {
            withComponents.Add(typeof(T));
            return this;
        }

        public View Without<T>() where T : IComponent
        {
            withoutComponents.Add(typeof(T));
            return this;
        }

        public override bool Equals(object? obj)
        {
            return obj is View view &&
                   EqualityComparer<ISet<Type>>.Default.Equals(withComponents, view.withComponents) &&
                   EqualityComparer<ISet<Type>>.Default.Equals(withoutComponents, view.withoutComponents);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(withComponents, withoutComponents);
        }

        public bool IsInView(Entity entity)
        {
            foreach (var withComponent in withComponents)
            {
                if (!entity.ContainsComponent(withComponent))
                {
                    return false;
                }
            }
            foreach (var withoutComponent in withoutComponents)
            {
                if (entity.ContainsComponent(withoutComponent))
                {
                    return false;
                }
            }
            return true;
        }

        public void Add(Entity entity)
        {
            if (!TrackedEntities.Contains(entity))
            {
                ToAddEntities.Add(entity);
            }
        }

        public void Remove(Entity entity)
        {
            if (TrackedEntities.Contains(entity))
            {
                ToRemoveEntities.Add(entity);
            }
        }

        public void ApplyQueued()
        {
            TrackedEntities.UnionWith(ToAddEntities);
            AddedEntities.UnionWith(ToAddEntities);
            ToAddEntities.Clear();

            TrackedEntities.ExceptWith(ToRemoveEntities);
            RemovedEntities.UnionWith(ToRemoveEntities);
            ToRemoveEntities.Clear();
        }

        public void ClearStates()
        {
            AddedEntities.Clear();
            RemovedEntities.Clear();
            ChangedEntities.Clear();
        }
    }
}