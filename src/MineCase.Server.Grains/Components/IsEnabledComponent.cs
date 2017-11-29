using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Components
{
    internal class IsEnabledComponent : Component, IHandle<Disable>, IHandle<Enable>
    {
        public static readonly DependencyProperty<bool> IsEnabledProperty =
            DependencyProperty.Register<bool>(nameof(IsEnabled), typeof(IsEnabledComponent));

        public bool IsEnabled => AttachedObject.GetValue(IsEnabledProperty);

        public IsEnabledComponent(string name = "isEnabled")
            : base(name)
        {
        }

        Task IHandle<Enable>.Handle(Enable message)
        {
            AttachedObject.SetLocalValue(IsEnabledProperty, true);
            return Task.CompletedTask;
        }

        Task IHandle<Disable>.Handle(Disable message)
        {
            AttachedObject.SetLocalValue(IsEnabledProperty, false);
            return Task.CompletedTask;
        }
    }
}
