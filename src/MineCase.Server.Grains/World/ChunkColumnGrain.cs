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
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class ChunkColumnGrain : Grain, IChunkColumn
    {
        private IWorld _world;
        private int _chunkX;
        private int _chunkZ;

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

        public override Task OnActivateAsync()
        {
            var key = this.GetWorldAndChunkPosition();
            _world = GrainFactory.GetGrain<IWorld>(key.worldKey);
            _chunkX = key.x;
            _chunkZ = key.z;
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
                var blockWorldPos = chunkPos.ToBlockWorldPos(new ChunkWorldPos(_chunkX, _chunkZ));
                await GetBroadcastGenerator().BlockChange(blockWorldPos, blockState);

                if (oldState.Id != blockState.Id)
                {
                    bool replaceOld = true;
                    var newEntity = BlockEntity.Create(GrainFactory, _world, blockWorldPos, (BlockId)blockState.Id);

                    // 删除旧的 BlockEntity
                    if (_blockEntities.TryGetValue(chunkPos, out var entity))
                    {
                        if (newEntity != null && entity.GetPrimaryKeyString() == newEntity.GetPrimaryKeyString())
                            replaceOld = false;

                        if (replaceOld)
                        {
                            await entity.Destroy();
                            _blockEntities.Remove(chunkPos);
                        }
                    }

                    // 添加新的 BlockEntity
                    if (newEntity != null && replaceOld)
                    {
                        _blockEntities.Add(chunkPos, newEntity);
                        await newEntity.OnCreated();
                    }
                }

                // 通知周围 Block 更改
                await Task.WhenAll(CrossCoords.Select(crossCoord =>
               {
                   var neighborPos = blockWorldPos;
                   neighborPos.X += crossCoord.x;
                   neighborPos.Z += crossCoord.z;
                   var chunk = neighborPos.GetChunk();
                   var blockChunkPos = neighborPos.ToBlockChunkPos();
                   return GrainFactory.GetGrain<IChunkColumn>(_world.MakeChunkColumnKey(chunk.chunkX, chunk.chunkZ)).OnBlockNeighborChanged(
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
                    _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
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

        public Task OnBlockNeighborChanged(int x, int y, int z, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState)
        {
            var block = _state[x, y, z];
            var blockHandler = BlockHandler.Create((BlockId)block.Id);
            var selfPosition = new BlockChunkPos(x, y, z).ToBlockWorldPos(new ChunkWorldPos(_chunkX, _chunkZ));
            return blockHandler.OnNeighborChanged(selfPosition, neighborPosition, oldState, newState, GrainFactory, _world);
        }
    }
}