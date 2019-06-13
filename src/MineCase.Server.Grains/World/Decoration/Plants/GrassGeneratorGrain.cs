using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class GrassGeneratorGrain : PlantsGeneratorGrain, IGrassGenerator
    {
        public override async Task GenerateSingle(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            BlockChunkPos chunkPos = pos.ToBlockChunkPos();
            int x = chunkPos.X;
            int y = chunkPos.Y;
            int z = chunkPos.Z;

            // TODO use block accessor
            var curBlock = await GetBlock(world, chunk, chunkWorldPos, pos);
            var downBlock = await GetBlock(world, chunk, chunkWorldPos, pos);
            if (curBlock.IsAir() &&
                downBlock == BlockStates.GrassBlock())
            {
                SetBlock(world, chunk, chunkWorldPos, pos, BlockStates.Grass(GrassType.TallGrass));
            }
        }
    }
}
