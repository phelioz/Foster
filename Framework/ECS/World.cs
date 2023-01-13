using System.Collections.Generic;

namespace Foster.Framework
{
    public class State
    {
        internal readonly Dictionary<string, Entity> entities = new();
        public ECS? Engine { get; internal set; }

        public void AddEntity(Entity entity)
        {
            if (Engine != null)
            {
                Engine.AddEntity(entity);
            }
            else
            {
                entities.Add(entity.ID, entity);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            if (Engine != null)
            {
                Engine.RemoveEntity(entity);
            }
            else
            {
                entities.Remove(entity.ID);
            }
        }

        public void RemoveEntity(string ID)
        {
            if (Engine != null)
            {
                Engine.RemoveEntity(ID);
            }
            else
            {
                entities.Remove(ID);
            }
        }
    }
}
