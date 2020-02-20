using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Util.Math;
using MineCase.World;
using Orleans;
using Orleans.Runtime;

namespace MineCase.Server.World
{
    public interface IAddressByPartition : IGrainWithStringKey
    {
    }

    public static class AddressByPartitionExtensions
    {
        public static string MakeAddressByPartitionKey(this IWorld world, ChunkPos chunkWorldPos)
        {
            return $"{world.GetPrimaryKeyString()},{chunkWorldPos.X},{chunkWorldPos.Z}";
        }

        public static TGrainInterface GetPartitionGrain<TGrainInterface>(this IGrainFactory grainFactory, IWorld world, ChunkPos chunkWorldPos)
            where TGrainInterface : IAddressByPartition
        {
            return grainFactory.GetGrain<TGrainInterface>(MakeAddressByPartitionKey(world, chunkWorldPos));
        }

        public static TGrainInterface GetPartitionGrain<TGrainInterface>(this IGrainFactory grainFactory, IAddressByPartition another)
            where TGrainInterface : IAddressByPartition
        {
            return grainFactory.GetGrain<TGrainInterface>(another.GetPrimaryKeyString());
        }

        public static ChunkPos GetChunkPos(this IAddressByPartition addressByPartition)
        {
            var key = addressByPartition.GetPrimaryKeyString().Split(',');
            return new ChunkPos(int.Parse(key[1]), int.Parse(key[2]));
        }

        public static (string worldKey, ChunkPos chunkPos) GetWorldAndChunkPos(this IAddressByPartition addressByPartition)
        {
            var key = addressByPartition.GetPrimaryKeyString().Split(',');
            return (key[0], new ChunkPos(int.Parse(key[1]), int.Parse(key[2])));
        }
    }
}
