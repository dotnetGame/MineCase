using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    public interface IEffectiveValue
    {
        AsyncEventHandler<IEffectiveValueChangedEventArgs> ValueChanged { set; }
    }

    public interface IEffectiveValue<T> : IEffectiveValue
    {
        bool CanSetValue { get; }

        T Value { get; }

        Task SetValue(T value);
    }

    public interface IEffectiveValueChangedEventArgs
    {
        object OldValue { get; }

        object NewValue { get; }
    }

    public class EffectiveValueChangedEventArgs<T> : EventArgs, IEffectiveValueChangedEventArgs
    {
        public T OldValue { get; }

        public T NewValue { get; }

        object IEffectiveValueChangedEventArgs.OldValue => OldValue;

        object IEffectiveValueChangedEventArgs.NewValue => NewValue;

        public EffectiveValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
