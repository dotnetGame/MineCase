using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Decoration.Plants
{
    public interface IPlantsGenerator : IGrainWithIntegerKey
    {
        Task GenerateSingle(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);

        Task Generate(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos pos, int countPerChunk, int range);
    }
}
