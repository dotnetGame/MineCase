using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Core.World;
using MineCase.Server.Interfaces.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Grains.World
{
    public class PartitionState
    {
        public string WorldName { get; set; }

        public BlockWorldPos Position { get; set; }

        // public EntityPack Entities { get; set; }
        public bool IsActive { get; set; }
    }

    public class WorldPartitionGrain : Grain, IWorldPartition
    {
        public static readonly int RangeAOI = 16;

        public static readonly int PartitionSize = 16; // 256 x 256 block

        private string _worldName;

        private BlockWorldPos _position;

        private bool _isActive = false;

        private ChunkColumn[,] _chunkColumns = new ChunkColumn[WorldPartitionGrain.PartitionSize, WorldPartitionGrain.PartitionSize];

        public override async Task OnActivateAsync()
        {
            var keys = this.GetWorldAndPartitionPos();
            _worldName = keys.worldKey;
            _position = keys.partitionPos;

            await base.OnActivateAsync();
        }

        public Task OnTick()
        {
            return Task.CompletedTask;
        }

        public static string MakeAddressByPartitionKey(IWorld world, BlockWorldPos blockWorldPos)
        {
            return $"{world.GetPrimaryKeyString()},{blockWorldPos.X},{blockWorldPos.Z}";
        }

        public static string MakeAddressByPartitionKey(string world, BlockWorldPos blockWorldPos)
        {
            return $"{world},{blockWorldPos.X},{blockWorldPos.Z}";
        }

        public BlockWorldPos GetPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return new BlockWorldPos(int.Parse(key[1]), 0, int.Parse(key[2]));
        }

        public (string worldKey, BlockWorldPos partitionPos) GetWorldAndPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return (key[0], new BlockWorldPos(int.Parse(key[1]), 0, int.Parse(key[2])));
        }
    }
}
