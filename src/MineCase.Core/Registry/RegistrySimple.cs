using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Registry
{
    public class RegistrySimple<TK, TV>
    {
        /** Objects registered on this registry. */
        protected readonly Dictionary<TK, TV> registryObjects;
        private TV[] values;

        public RegistrySimple()
        {
            registryObjects = new Dictionary<TK, TV>();
        }

        public virtual TV GetObject( TK name)
        {
            return this.registryObjects[name];
        }

        /**
         * Register an object on this registry.
         */
        public void PutObject(TK key, TV value)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (value == null)
            {
                throw new ArgumentNullException();
            }

            this.values = null;

            if (this.registryObjects.ContainsKey(key))
            {
                // LOGGER.debug("Adding duplicate key '{}' to registry", key);
            }

            registryObjects.Add(key, value);
        }

        /**
         * Gets all the keys recognized by this registry.
         */
        public Dictionary<TK, TV>.KeyCollection GetKeys()
        {
            return registryObjects.Keys;
        }

        public TV GetRandomObject(Random random)
        {
            if (values == null)
            {
                var collection = registryObjects.Values;

                if (collection.Count == 0)
                {
                    return default(TV);
                }

                values = new TV[collection.Count];
                collection.CopyTo(values, 0);
            }

            return values[random.Next(values.Length)];
        }

        /**
         * Does this registry contain an entry for the given key?
         */
        public bool ContainsKey(TK key)
        {
            return registryObjects.ContainsKey(key);
        }
    }
}
