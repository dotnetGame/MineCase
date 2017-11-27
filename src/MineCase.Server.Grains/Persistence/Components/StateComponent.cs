using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Engine.Serialization;

namespace MineCase.Server.Persistence.Components
{
    public sealed class InitializeStateMark
    {
        public static readonly InitializeStateMark Default = new InitializeStateMark();
    }

    public class StateComponent<T> : Component, IHandle<AfterReadState>, IHandle<BeforeWriteState>
    {
        public static readonly DependencyProperty<T> StateProperty =
            DependencyProperty.Register<T>(nameof(State), typeof(StateComponent<T>));

        public T State => AttachedObject.GetValue(StateProperty);

        public event AsyncEventHandler<EventArgs> BeforeWriteState;

        public event AsyncEventHandler<EventArgs> AfterReadState;

        public StateComponent(string name = "state")
            : base(name)
        {
        }

        async Task IHandle<AfterReadState>.Handle(AfterReadState message)
        {
            // 如果为 null 需要初始化状态
            if (State == null)
                await AttachedObject.SetLocalValue(StateProperty, (T)Activator.CreateInstance(typeof(T), InitializeStateMark.Default));

            await AfterReadState.InvokeSerial(this, EventArgs.Empty);
        }

        Task IHandle<BeforeWriteState>.Handle(BeforeWriteState message)
        {
            return BeforeWriteState.InvokeSerial(this, EventArgs.Empty);
        }
    }
}
