using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.Util.Event;
using MineCase.Util.Math;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World
{
    public class WorldPartitionState
    {
    }

    public class WorldPartition : Grain<WorldPartitionState>, IWorldPartition
    {
        private IDisposable _tickTimer;
        private long _worldAge;

        private static readonly long _updateMs = 50;

        private IWorld _world = null;

        private WorldPartitionData _partition;

        private WorldAccessor _worldAccessor;

        public event AsyncEventHandler<GameTickArgs> Tick;

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            (string worldKey, ChunkPos chunkPos) = this.GetWorldAndChunkPos();
            _world = GrainFactory.GetGrain<IWorld>(worldKey);
            _partition = new WorldPartitionData();
            _worldAccessor = new WorldAccessor(GrainFactory);
        }

        public override async Task OnDeactivateAsync()
        {
            await WriteStateAsync();
            await base.OnDeactivateAsync();
        }

        public async Task StartTick()
        {
            _tickTimer?.Dispose();
            _worldAge = await _world.GetAge();

            _tickTimer = RegisterTimer(OnTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_updateMs));
        }

        public async Task OnTick(object arg)
        {
            var e = new GameTickArgs { WorldAge = _worldAge, TimeOfDay = _worldAge % 24000 };
            await Tick.InvokeSerial(this, e);
            _worldAge++;
        }

        public Task StopTick()
        {
            _tickTimer?.Dispose();
            _tickTimer = null;
            return Task.CompletedTask;
        }
    }
}
