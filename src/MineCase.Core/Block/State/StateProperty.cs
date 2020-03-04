using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.State
{
    public abstract class StateProperty<T> : IStateProperty
    {
        public string Name { get; set; }

        public string GetName()
        {
            return Name;
        }

        public abstract int StateNumber();

        public abstract int ToInt(T value);
    }
}
