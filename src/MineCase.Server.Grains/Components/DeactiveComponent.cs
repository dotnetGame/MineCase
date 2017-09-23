using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Components
{
    internal class DeactiveComponent : Component, IHandle<Disable>
    {
        public override int GetMessageOrder(object message)
        {
            if (message is Disable)
                return int.MaxValue;
            return base.GetMessageOrder(message);
        }

        public DeactiveComponent(string name = "deactive")
            : base(name)
        {
        }

        Task IHandle<Disable>.Handle(Disable message)
        {
            AttachedObject.Destroy();
            return Task.CompletedTask;
        }
    }
}
