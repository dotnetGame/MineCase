using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Generation;
using Orleans;

namespace MineCase.Server.World
{
    internal class ChunkColumnGrain : Grain, IChunkColumn
    {
        private IWorld _world;
        private int _chunkX;
        private int _chunkZ;

        private ChunkColumnStorage _state;

        public Task<ChunkColumnStorage> GetState() => Task.FromResult(_state);
        /*
        var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(1);
        GeneratorSettings settings = new GeneratorSettings
        {
            Seed = 1,
        };
        ChunkColumn chunkColumn = await generator.Generate(_chunkX, _chunkZ, settings);
        return chunkColumn;
        */

        /*
        var blocks = new Block[16 * 16 * 16];
        var index = 0;
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    if (y == 0)
                        blocks[index] = new Block { Id = 1, SkyLight = 0xF };
                    else
                        blocks[index] = new Block { Id = 0, SkyLight = 0xF };
                    index++;
                }
            }
        }

        return Task.FromResult(new ChunkColumn
        {
            Biomes = Enumerable.Repeat<byte>(0, 256).ToArray(),
            SectionBitMask = 0b1111_1111_1111_1111,
            Sections = new[]
            {
                new ChunkSection
                {
                    BitsPerBlock = 13,
                    Blocks = blocks
                }
            }.Concat(Enumerable.Repeat(
                new ChunkSection
                {
                    BitsPerBlock = 13,
                    Blocks = Enumerable.Repeat(new Block { Id = 0, SkyLight = 0xF }, 16 * 16 * 16).ToArray()
                }, 15)).ToArray()
        });
        */

        public override async Task OnActivateAsync()
        {
            var key = this.GetWorldAndChunkPosition();
            _world = GrainFactory.GetGrain<IWorld>(key.worldKey);
            _chunkX = key.x;
            _chunkZ = key.z;

            await EnsureChunkGenerated();
        }

        public Task<BlockState> GetBlockState(int x, int y, int z)
        {
            return Task.FromResult(_state[x, y, z]);
        }

        public Task SetBlockState(int x, int y, int z, BlockState state)
        {
            _state[x, y, z] = state;
            return Task.CompletedTask;
        }

        private async Task EnsureChunkGenerated()
        {
            /*
            var generator = GrainFactory.GetGrain<IChunkGeneratorFlat>(1);
            GeneratorSettings settings = new GeneratorSettings
            {
                FlatGeneratorInfo = new FlatGeneratorInfo
                {
                    FlatBlockId = new BlockState?[] { BlockStates.Stone(), BlockStates.Dirt(), BlockStates.Grass() }
                }
            };
            _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
            */
            int seed = await _world.GetSeed();
            GeneratorSettings settings = await _world.GetGeneratorSettings();
            var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(seed);
            _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
        }
    }
}
