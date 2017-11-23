using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;

namespace MineCase.Server.Persistence.Components
{
    internal class AutoSaveStateComponent : Component
    {
        private readonly int _periodTime;

        public const int PerMinute = 20 * 60;

        public bool IsDirty { get; set; }

        public AutoSaveStateComponent(int periodTime, string name = "autoSaveState")
            : base(name)
        {
            _periodTime = periodTime;
        }

        protected override Task OnAttached()
        {
            var tickComponent = AttachedObject.GetComponent<GameTickComponent>();
            if (tickComponent != null)
                tickComponent.Tick += OnGameTick;
            return base.OnAttached();
        }

        public async Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {
            if (IsDirty && (e.worldAge % _periodTime == 0))
            {
                IsDirty = false;
                await AttachedObject.WriteStateAsync();
            }
        }
    }
}
