using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Registry
{
    public class RegistryNamespaced<TK, TV> : RegistrySimple<TK, TV>
    {
        /** The backing store that maps Integers to objects. */
        protected readonly Dictionary<int, TV> underlyingIntegerMap = new Dictionary<int, TV>();

        protected readonly Dictionary<TV, int> underlyingInversedIntegerMap = new Dictionary<TV, int>();

        /** A BiMap of objects (key) to their names (value). */
        protected readonly Dictionary<TV, TK> inverseObjectRegistry;

        public RegistryNamespaced()
        {
            // this.inverseObjectRegistry = ((BiMap)this.registryObjects).inverse();
        }

        public void Register(int id, TK key, TV value)
        {
            underlyingIntegerMap.Add(id, value);
            underlyingInversedIntegerMap.Add(value, id);
            PutObject(key, value);
        }

        public override TV GetObject(TK name)
        {
            return GetObject(name);
        }

        /**
         * Gets the name we use to identify the given object.
         */
        public TK GetNameForObject(TV value)
        {
            return inverseObjectRegistry[value];
        }

        /**
         * Does this registry contain an entry for the given key?
         */
        public new bool ContainsKey(TK key)
        {
            return base.ContainsKey(key);
        }

        /**
         * Gets the integer ID we use to identify the given object.
         */
        public int GetIDForObject(TV value)
        {
            return underlyingInversedIntegerMap[value];
        }

        /**
         * Gets the object identified by the given ID.
         */
        public TV GetObjectById(int id)
        {
            return underlyingIntegerMap[id];
        }
    }
}
