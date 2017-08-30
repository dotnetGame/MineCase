using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class WorldGrain : Grain, IWorld
    {
        private Dictionary<uint, IEntity> _entities;
        private uint _nextAvailEId;
        private long _worldAge;

        public override Task OnActivateAsync()
        {
            _nextAvailEId = 0;
            _entities = new Dictionary<uint, IEntity>();
            return base.OnActivateAsync();
        }

        public Task AttachEntity(IEntity entity)
        {
            _entities.Add(entity.GetEntityId(), entity);
            return Task.CompletedTask;
        }

        public Task<IEntity> FindEntity(uint eid)
        {
            if (_entities.TryGetValue(eid, out var entity))
                return Task.FromResult(entity);
            return Task.FromException<IEntity>(new EntityNotFoundException());
        }

        public Task<(long age, long timeOfDay)> GetTime()
        {
            return Task.FromResult((_worldAge, _worldAge % 24000));
        }

        public Task<uint> NewEntityId()
        {
            var id = _nextAvailEId++;
            return Task.FromResult(id);
        }

        public Task OnGameTick(TimeSpan deltaTime)
        {
            _worldAge += 20;
            return Task.CompletedTask;
        }

        public Task<long> GetAge() => Task.FromResult(_worldAge);

        public async Task<BlockState> GetBlockState(int x, int y, int z)
        {
            // 需要优化？？
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(this.MakeChunkColumnKey(x, z));

            return await chunkColumn.GetBlockState(x, y, z);
        }

        public async Task SetBlockState(BlockState state, int x, int y, int z)
        {
            // 需要优化？？
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(this.MakeChunkColumnKey(x, z));
            await chunkColumn.SetBlockState(state, x, y, z)
        }
    }
}
