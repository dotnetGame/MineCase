using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;

namespace MineCase.Engine
{
    internal interface IDependencyPropertyHelper
    {
        object GetValue(IEffectiveValue effectiveValue);

        IEffectiveValue FromValue(object value);
    }

    internal sealed class DependencyPropertyHelper<T> : IDependencyPropertyHelper
    {
        public IEffectiveValue FromValue(object value)
        {
            return LocalDependencyValueProvider.FromValue<T>((T)value);
        }

        public object GetValue(IEffectiveValue effectiveValue)
        {
            return ((IEffectiveValue<T>)effectiveValue).Value;
        }
    }
}
