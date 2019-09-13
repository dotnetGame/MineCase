using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Interfaces.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Grains.World
{
    public class PartitionState
    {
        public string WorldName { get; set; }

        public BlockWorldPos Position { get; set; }

        public EntityPack Entities { get; set; }

        public bool IsActive { get; set; }
    }

    public class WorldPartitionGrain : Grain, IWorldPartition
    {
        public const int RangeAOI = 16;

        public const int PartitionSize = 16 * 16; // 256 x 256 block

        private string _worldName;

        private BlockWorldPos _position;

        private bool _isActive = false;

        private EntityPack _entityPack = new EntityPack();

        private Dictionary<string, PartitionState> _ghostPartitions = new Dictionary<string, PartitionState>();

        public override async Task OnActivateAsync()
        {
            var keys = this.GetWorldAndPartitionPos();
            _worldName = keys.worldKey;
            _position = keys.partitionPos;

            await base.OnActivateAsync();
        }

        public Task EnterEntity(Entity entity)
        {
            _entityPack.AddEntity(entity);
            return Task.CompletedTask;
        }

        public Task LeaveEntity(Entity entity)
        {
            _entityPack.RemoveEntity(entity.GetGuid());
            return Task.CompletedTask;
        }

        public Task OnTick()
        {
            return Task.CompletedTask;
        }

        private string MakeAddressByPartitionKey(IWorld world, BlockWorldPos blockWorldPos)
        {
            return $"{world.GetPrimaryKeyString()},{blockWorldPos.X},{blockWorldPos.Z}";
        }

        private BlockWorldPos GetPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return new BlockWorldPos(int.Parse(key[1]), 0, int.Parse(key[2]));
        }

        private (string worldKey, BlockWorldPos partitionPos) GetWorldAndPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return (key[0], new BlockWorldPos(int.Parse(key[1]), 0, int.Parse(key[2])));
        }
    }
}
