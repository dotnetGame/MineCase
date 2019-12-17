using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.World
{
    public class TickEmitterGrain : AddressByPartitionGrain, ITickEmitter
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
            var keys = this.GetWorldAndChunkWorldPos();
            var partition = GrainFactory.GetPartitionGrain<IWorldPartition>(GrainFactory.GetGrain<IWorld>(keys.worldKey), keys.chunkWorldPos);
            return partition.OnTick();
        }

        public Task Stop()
        {
            _timer?.Dispose();
            _timer = null;
            return Task.CompletedTask;
        }
    }
}
