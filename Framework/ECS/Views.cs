using System.Collections;
using System.Collections.Generic;

namespace Foster.Framework
{
    public class Views : IEnumerable
    {
        private readonly ISet<View> views = new HashSet<View>();

        public Views AddView(View view)
        {
            views.Add(view);
            return this;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)views).GetEnumerator();
        }
    }
}