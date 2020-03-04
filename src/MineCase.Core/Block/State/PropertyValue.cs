using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.State
{
    public class PropertyValue
    {
        private IStateProperty _property;

        private string _value;

        public PropertyValue(IStateProperty prop, string value)
        {
            _property = prop;
            _value = value;
        }

        public override int GetHashCode()
        {
            var hashCode = -81208087;
            hashCode = hashCode * -1521134295 + _property.GetHashCode();
            hashCode = hashCode * -1521134295 + _value.GetHashCode();
            return hashCode;
        }
    }
}
