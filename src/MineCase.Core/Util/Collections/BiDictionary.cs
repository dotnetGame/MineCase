using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Collections
{
    public class BiDictionary<T, U>
    {
        private Dictionary<T, U> _firstMap = new Dictionary<T, U>();

        private Dictionary<U, T> _secondMap = new Dictionary<U, T>();

        public BiDictionary(Dictionary<T, U> firstMap, Dictionary<U, T> secondMap)
        {
            _firstMap = firstMap;
            _secondMap = secondMap;
        }

        public void Add(T first, U second)
        {
            _firstMap[first] = second;
            _secondMap[second] = first;
        }

        public U GetFirst(T value)
        {
            return _firstMap[value];
        }

        public T GetSecond(U value)
        {
            return _secondMap[value];
        }
    }
}
