using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network.Play;
using MineCase.Server.Settings;
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
        private Dictionary<BlockChunkPos, IBlockEntity> _blockEntities;

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
            _blockEntities = new Dictionary<BlockChunkPos, IBlockEntity>();
            return Task.CompletedTask;
        }

        public async Task SetBlockState(int x, int y, int z, BlockState blockState)
        {
            await EnsureChunkGenerated();
            _state[x, y, z] = blockState;

            var chunkPos = new BlockChunkPos(x, y, z);
            var blockWorldPos = chunkPos.ToBlockWorldPos(new ChunkWorldPos(_chunkX, _chunkZ));
            await GetBroadcastGenerator().BlockChange(blockWorldPos, blockState);

            // 删除旧的 BlockEntity
            if (_blockEntities.TryGetValue(chunkPos, out var entity))
            {
                await entity.Destroy();
                _blockEntities.Remove(chunkPos);
            }

            // 添加新的 BlockEntity
            entity = BlockEntity.Create(GrainFactory, _world, blockWorldPos, (BlockId)blockState.Id);
            if (entity != null)
                _blockEntities.Add(chunkPos, entity);
        }

        private async Task EnsureChunkGenerated()
        {
            if (!_generated)
            {
                var serverSetting = GrainFactory.GetGrain<IServerSettings>(0);
                string worldType = (await serverSetting.GetSettings()).LevelType;
                if (worldType == "DEFAULT" || worldType == "default")
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await _world.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                    };
                    _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
                }
                else if (worldType == "FLAT" || worldType == "flat")
                {
                    if (worldType == "DEFAULT" || worldType == "default")
                    {
                        var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await _world.GetSeed());
                        GeneratorSettings settings = new GeneratorSettings
                        {
                        };
                        _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
                    }
                    else if (worldType == "FLAT" || worldType == "flat")
                    {
                        var generator = GrainFactory.GetGrain<IChunkGeneratorFlat>(await _world.GetSeed());
                        GeneratorSettings settings = new GeneratorSettings
                        {
                            FlatBlockId = new BlockState?[] { BlockStates.Stone(), BlockStates.Dirt(), BlockStates.Grass() }
                        };
                        _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
                    }
                    else
                    {
                        var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await _world.GetSeed());
                        GeneratorSettings settings = new GeneratorSettings
                        {
                        };
                        _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
                    }
                }

                _generated = true;
            }
        }

        protected ClientPlayPacketGenerator GetBroadcastGenerator()
        {
            return new ClientPlayPacketGenerator(GrainFactory.GetGrain<IChunkTrackingHub>(_world.MakeChunkTrackingHubKey(_chunkX, _chunkZ)));
        }

        public Task<IBlockEntity> GetBlockEntity(int x, int y, int z)
        {
            if (_blockEntities.TryGetValue(new BlockChunkPos(x, y, z), out var entity))
                return Task.FromResult(entity);
            return Task.FromResult<IBlockEntity>(null);
        }
    }
}