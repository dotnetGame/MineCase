using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.World;

namespace MineCase.Server.Persistence.Components
{
    internal class PeriodicSaveStateComponent : Component
    {
        private readonly TimeSpan _periodTime;

        public PeriodicSaveStateComponent(TimeSpan periodTime, string name = "periodicSaveState")
            : base(name)
        {
            _periodTime = periodTime;
        }

        protected override void OnAttached()
        {
            AttachedObject.RegisterTimer(SaveIfDirty, null, _periodTime, _periodTime);
        }

        private Task SaveIfDirty(object state)
        {
            if (AttachedObject.ValueStorage.IsDirty)
            {
                return AttachedObject.WriteStateAsync();
            }

            return Task.CompletedTask;
        }
    }
}
