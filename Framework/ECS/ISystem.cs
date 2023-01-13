using System.Collections.Generic;

namespace Foster.Framework
{
    public interface ISystem
    {
        public Views GetViews();
        public Dependencies GetDependencies();

        public void OnBegin() { }
        public void OnEnd() { }

        public void OnAdd(View view, IEnumerable<Entity> entities) { }
        public void OnRemove(View view, IEnumerable<Entity> entities) { }
        public void OnUpdate(View view, IEnumerable<Entity> entities, float deltaTime) { }
        public void OnUpdateFixed(View view, IEnumerable<Entity> entities, float deltaTime) { }
    }
}
