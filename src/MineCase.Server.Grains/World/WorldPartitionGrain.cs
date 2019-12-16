using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Chunk;
using Orleans;

namespace MineCase.Server.World
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

        private ChunkWorldPos _position;

        private bool _isActive = false;

        private ChunkColumn[,] _chunkColumns;

        public WorldPartitionGrain()
        {
            _chunkColumns = new ChunkColumn[WorldPartitionGrain.PartitionSize, WorldPartitionGrain.PartitionSize];
            for (int x = 0; x < PartitionSize; ++x)
            {
                for (int z = 0; z < PartitionSize; ++z)
                {
                    _chunkColumns[x, z] = new ChunkColumn();
                }
            }
        }

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

        public static string MakeAddressByPartitionKey(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            return $"{world.GetPrimaryKeyString()},{chunkWorldPos.X},{chunkWorldPos.Z}";
        }

        public static string MakeAddressByPartitionKey(string world, ChunkWorldPos chunkWorldPos)
        {
            return $"{world},{chunkWorldPos.X},{chunkWorldPos.Z}";
        }

        public ChunkWorldPos GetPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return new ChunkWorldPos(int.Parse(key[1]), int.Parse(key[2]));
        }

        public (string worldKey, ChunkWorldPos partitionPos) GetWorldAndPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return (key[0], new ChunkWorldPos(int.Parse(key[1]), int.Parse(key[2])));
        }

        public Task<ChunkColumn> GetState(ChunkWorldPos pos)
        {
            if (pos.X >= _position.X && pos.Z >= _position.Z &&
                pos.X < _position.X + PartitionSize && pos.Z < _position.Z + PartitionSize)
            {
                return Task.FromResult(_chunkColumns[pos.X - _position.X, pos.Z - _position.Z]);
            }
            else
            {
                return Task.FromResult((ChunkColumn)null);
            }
        }
    }
}
