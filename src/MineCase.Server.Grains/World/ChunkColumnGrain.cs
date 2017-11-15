using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network.Play;
using MineCase.Server.Settings;
using MineCase.Server.World.Generation;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class ChunkColumnGrain : Grain, IChunkColumn
    {
        private IWorld _world;
        private ChunkWorldPos _chunkPos;

        private bool _generated = false;
        private ChunkColumnCompactStorage _state;
        private Dictionary<BlockChunkPos, IBlockEntity> _blockEntities;

        public async Task<BlockState> GetBlockState(int x, int y, int z)
        {
            await EnsureChunkGenerated();
            return _state[x, y, z];
        }

        public async Task<ChunkColumnCompactStorage> GetState()
        {
            await EnsureChunkGenerated();
            return _state;
        }

        public async Task<BiomeId> GetBlockBiome(int x, int z)
        {
            await EnsureChunkGenerated();
            return (BiomeId)_state.Biomes[(z * ChunkConstants.BlockEdgeWidthInSection) + x];
        }

        public override Task OnActivateAsync()
        {
            var keys = this.GetWorldAndChunkWorldPos();
            _world = GrainFactory.GetGrain<IWorld>(keys.worldKey);
            _chunkPos = keys.chunkWorldPos;
            _blockEntities = new Dictionary<BlockChunkPos, IBlockEntity>();
            return Task.CompletedTask;
        }

        public static readonly (int x, int z)[] CrossCoords = new[]
        {
            (-1, 0), (0, -1), (1, 0), (0, 1)
        };

        public async Task SetBlockState(int x, int y, int z, BlockState blockState)
        {
            await EnsureChunkGenerated();
            var oldState = _state[x, y, z];

            if (oldState != blockState)
            {
                _state[x, y, z] = blockState;

                var chunkPos = new BlockChunkPos(x, y, z);
                var blockWorldPos = chunkPos.ToBlockWorldPos(_chunkPos);
                await GetBroadcastGenerator().BlockChange(blockWorldPos, blockState);

                if (oldState.Id != blockState.Id)
                {
                    bool replaceOld = true;
                    var newEntity = BlockEntity.Create(GrainFactory, (BlockId)blockState.Id);

                    // 删除旧的 BlockEntity
                    if (_blockEntities.TryGetValue(chunkPos, out var entity))
                    {
                        if (newEntity != null && entity.GetPrimaryKeyString() == newEntity.GetPrimaryKeyString())
                            replaceOld = false;

                        if (replaceOld)
                        {
                            await entity.Tell(DestroyBlockEntity.Default);
                            _blockEntities.Remove(chunkPos);
                        }
                    }

                    // 添加新的 BlockEntity
                    if (newEntity != null && replaceOld)
                    {
                        _blockEntities.Add(chunkPos, newEntity);
                        await newEntity.Tell(new SpawnBlockEntity { World = _world, Position = blockWorldPos });
                    }
                }

                // 通知周围 Block 更改
                await Task.WhenAll(CrossCoords.Select(crossCoord =>
               {
                   var neighborPos = blockWorldPos;
                   neighborPos.X += crossCoord.x;
                   neighborPos.Z += crossCoord.z;
                   var chunk = neighborPos.ToChunkWorldPos();
                   var blockChunkPos = neighborPos.ToBlockChunkPos();
                   return GrainFactory.GetPartitionGrain<IChunkColumn>(_world, chunk).OnBlockNeighborChanged(
                       blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z, blockWorldPos, oldState, blockState);
               }));
            }
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
                    _state = await generator.Generate(_world, _chunkPos.X, _chunkPos.Z, settings);
                }
                else if (worldType == "FLAT" || worldType == "flat")
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorFlat>(await _world.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                        FlatBlockId = new BlockState?[]
                        {
                            BlockStates.Bedrock(),
                            BlockStates.Stone(),
                            BlockStates.Stone(),
                            BlockStates.Dirt(),
                            BlockStates.Dirt(),
                            BlockStates.Grass()
                        }
                    };
                    _state = await generator.Generate(_world, _chunkPos.X, _chunkPos.Z, settings);
                }
                else
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await _world.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                    };
                    _state = await generator.Generate(_world, _chunkPos.X, _chunkPos.Z, settings);
                }

                _generated = true;
            }
        }

        protected ClientPlayPacketGenerator GetBroadcastGenerator()
        {
            return new ClientPlayPacketGenerator(GrainFactory.GetPartitionGrain<IChunkTrackingHub>(_world, _chunkPos));
        }

        public Task<IBlockEntity> GetBlockEntity(int x, int y, int z)
        {
            if (_blockEntities.TryGetValue(new BlockChunkPos(x, y, z), out var entity))
                return Task.FromResult(entity);
            return Task.FromResult<IBlockEntity>(null);
        }

        public Task OnBlockNeighborChanged(int x, int y, int z, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState)
        {
            var block = _state[x, y, z];
            var blockHandler = BlockHandler.Create((BlockId)block.Id);
            var selfPosition = new BlockChunkPos(x, y, z).ToBlockWorldPos(_chunkPos);
            return blockHandler.OnNeighborChanged(selfPosition, neighborPosition, oldState, newState, GrainFactory, _world);
        }
    }
}