using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Components
{
    internal class NameComponent : Component
    {
        public static readonly DependencyProperty<string> NameProperty =
            DependencyProperty.Register<string>("Name", typeof(NameComponent));

        public new string Name => AttachedObject.GetValue(NameProperty);

        public NameComponent(string name = "name")
            : base(name)
        {
        }

        public Task SetName(string value) =>
            AttachedObject.SetLocalValue(NameProperty, value);
    }
}
