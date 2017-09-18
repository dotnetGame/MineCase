using MineCase.Engine;
using MineCase.Server.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class KeepAliveComponent : Component
    {
        private uint _keepAliveId = 0;

        public KeepAliveComponent(string name = "keepAlive")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick += OnGameTick;
            return base.OnAttached();
        }

        public Task ReceiveRequest(uint id)
        {

        }

        public Task ReceiveReponse(uint id)
        {

        }

        private Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {

        }
    }
}
