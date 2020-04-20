using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.State
{
    public class EnumProperty<T> : StateProperty<T>
        where T : System.Enum
    {
        private Dictionary<T, int> _map;

        public EnumProperty()
        {
            _map = new Dictionary<T, int>();
            var enumValues = typeof(T).GetEnumValues();
            int index = 0;
            foreach (var value in enumValues)
            {
                _map[(T)value] = index;
                ++index;
            }
        }

        public override int StateNumber()
        {
            return typeof(T).GetEnumValues().Length;
        }

        public override int ToInt(T value)
        {
            return _map[value];
        }
    }
}
