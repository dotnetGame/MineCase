using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.Generation;
using Orleans;

namespace MineCase.Server.World
{
    internal class ChunkColumnGrain : Grain, IChunkColumn
    {
        private IWorld _world;
        private int _chunkX;
        private int _chunkZ;

        private bool _generated = false;
        private ChunkColumnStorage _state;

        public async Task<BlockState> GetBlockState(int x, int y, int z)
        {
            await EnsureChunkGenerated();
            return _state[x, y, z];
        }

        public async Task<ChunkColumnStorage> GetState()
        {
            await EnsureChunkGenerated();
            return _state;
        }

        public override Task OnActivateAsync()
        {
            var key = this.GetWorldAndChunkPosition();
            _world = GrainFactory.GetGrain<IWorld>(key.worldKey);
            _chunkX = key.x;
            _chunkZ = key.z;
            return Task.CompletedTask;
        }

        public async Task SetBlockState(int x, int y, int z, BlockState blockState)
        {
            await EnsureChunkGenerated();
            _state[x, y, z] = blockState;
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
            _state = await generator.Generate(_chunkX, _chunkZ, settings);
            */
            if (!_generated)
            {
                var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(1);
                GeneratorSettings settings = new GeneratorSettings
                {
                };
                _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
                _generated = true;
            }
        }
    }
}
