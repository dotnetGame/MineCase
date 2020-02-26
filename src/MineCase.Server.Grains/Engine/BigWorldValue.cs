using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Engine
{
    public interface IBigWorldValue
    {
        object GetValue();

        void SetValue(object value);
    }

    public class BigWorldValue<T> : IBigWorldValue
    {
        public T Value { get; set; }

        public object GetValue()
        {
            return Value;
        }

        public void SetValue(object value)
        {
            Value = (T)value;
        }
    }
}
