using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class GrassGeneratorGrain : PlantsGeneratorGrain, IGrassGenerator
    {
        public override async Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            // TODO use block accessor
            var curBlock = await GetBlock(world, chunkWorldPos, pos);
            var downBlock = await GetBlock(world, chunkWorldPos, new BlockWorldPos { X = pos.X, Y = pos.Y - 1, Z = pos.Z });
            if (curBlock.IsAir() &&
                downBlock == BlockStates.GrassBlock())
            {
                await SetBlock(world, chunkWorldPos, pos, BlockStates.Grass(GrassType.TallGrass));
            }
        }
    }
}
