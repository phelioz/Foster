using System;
using System.Collections.Generic;

namespace Foster.Framework
{
    public class Dependencies
    {
        readonly ISet<Type> dependencies = new HashSet<Type>();

        public readonly static Dependencies None = new();

        public Dependencies AddDependency<T>()
        {
            dependencies.Add(typeof(T));
            return this;
        }
    }
}