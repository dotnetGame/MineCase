using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;

namespace MineCase.Server.World.Decoration.Biomes
{
    public class BiomeSavannaDecoratorGrain : BiomeDecoratorGrain, IBiomeSavannaDecorator
    {
        public override Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }

        public override Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }

        public override Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }
    }
}
