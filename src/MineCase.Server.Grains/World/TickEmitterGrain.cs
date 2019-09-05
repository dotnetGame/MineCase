using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Interfaces.World;
using Orleans;

namespace MineCase.Server.Grains.World
{
    public class TickEmitterGrain : Grain, ITickEmitter
    {
        private IDisposable _timer;

        private static readonly long _updateMs = 50;

        public TickEmitterGrain()
        {
        }

        public Task Start()
        {
            _timer?.Dispose();
            _timer = RegisterTimer(OnTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_updateMs));
            return Task.CompletedTask;
        }

        private Task OnTick(object state)
        {
            var worldName = this.GetPrimaryKeyString();
            var world = GrainFactory.GetGrain<IWorld>(worldName);
            return world.OnTick();
        }

        public Task Stop()
        {
            _timer?.Dispose();
            _timer = null;
            return Task.CompletedTask;
        }
    }
}
