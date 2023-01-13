using System;
using System.Collections.Generic;

namespace Foster.Framework
{
    public class Entity : IEquatable<Entity?>
    {
        public readonly string ID = Guid.NewGuid().ToString();
        readonly ISet<Entity> entities = new HashSet<Entity>();
        readonly Dictionary<Type, IComponent> components = new();
        public State World { get; internal set; }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public bool RemoveEntity(Entity entity)
        {
            return entities.Remove(entity);
        }

        public bool ContainsEntity(Entity entity)
        {
            return entities.Contains(entity);
        }

        public void AddComponent(IComponent component)
        {
            var type = component.GetType();
            if (components.ContainsKey(type))
            {
                components.Remove(type);
            }
            components.Add(type, component);
        }

        public bool RemoveComponent<T>() where T : IComponent
        {
            return components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)components[typeof(T)];
        }

        public bool ContainsComponent<T>() where T : IComponent
        {
            return components.ContainsKey(typeof(T));
        }

        public bool ContainsComponent(Type copmonentType)
        {
            return components.ContainsKey(copmonentType);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Entity);
        }

        public bool Equals(Entity? other)
        {
            return other != null &&
                   ID == other.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return EqualityComparer<Entity>.Default.Equals(left, right);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }
    }
}
