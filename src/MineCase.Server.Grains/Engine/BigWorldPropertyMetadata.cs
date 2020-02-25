using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Engine
{
    public class BigWorldPropertyMetadata<T>
    {
        private bool _defaultValueSet;
        private T _defaultValue;

        public bool HasDefaultValue => _defaultValueSet;

        public T DefaultValue => _defaultValue;

        public BigWorldPropertyMetadata(T defaultValue)
        {
            _defaultValue = defaultValue;
            _defaultValueSet = true;
        }
    }
}
