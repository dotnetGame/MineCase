using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entity;
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

    public class WorldPartitionGrain : AddressByPartitionGrain, IWorldPartition
    {
        public static readonly int RangeAOI = 16;

        public static readonly int PartitionSize = 16; // 256 x 256 block

        private string _worldName;

        private ChunkWorldPos _position;

        private bool _isActive = false;

        private ChunkColumn[,] _chunkColumns;

        private List<EntityBase> _entities;

        // read-only
        private List<EntityBase> _ghostEntities;

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
            var keys = this.GetWorldAndChunkWorldPos();
            _worldName = keys.worldKey;
            _position = keys.chunkWorldPos;

            await base.OnActivateAsync();
        }

        public Task OnTick()
        {
            return Task.CompletedTask;
        }

        public ChunkWorldPos GetPartitionPos()
        {
            var key = this.GetPrimaryKeyString().Split(',');
            return new ChunkWorldPos(int.Parse(key[1]), int.Parse(key[2]));
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

    public static class ChunkWorldPosExtension
    {
        public static ChunkWorldPos ToPartitionWorldPos(this ChunkWorldPos pos)
        {
            var ret = new ChunkWorldPos { X = 0, Z = 0 };
            if (pos.X > 0)
            {
                ret.X = (pos.X / WorldPartitionGrain.PartitionSize) * WorldPartitionGrain.PartitionSize;
            }
            else
            {
                ret.X = -(((-pos.X - 1) / WorldPartitionGrain.PartitionSize) + 1) * WorldPartitionGrain.PartitionSize;
            }

            if (pos.Z > 0)
            {
                ret.Z = (pos.Z / WorldPartitionGrain.PartitionSize) * WorldPartitionGrain.PartitionSize;
            }
            else
            {
                ret.Z = -(((-pos.Z - 1) / WorldPartitionGrain.PartitionSize) + 1) * WorldPartitionGrain.PartitionSize;
            }

            return ret;
        }
    }
}
