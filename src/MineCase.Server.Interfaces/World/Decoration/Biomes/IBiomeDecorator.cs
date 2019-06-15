using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Server.World.Decoration.Biomes
{
    public interface IBiomeDecorator : IGrainWithIntegerKey
    {
        Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings);

        Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings);

        Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings);
    }
}
