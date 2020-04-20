using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Collections
{
    public class ObjectIntDictionary<T>
    {
        private Dictionary<T, int> _dict = new Dictionary<T, int>();

        public void Add(T obj, int value)
        {
            _dict.Add(obj, value);
        }
    }
}
