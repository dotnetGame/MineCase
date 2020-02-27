using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network.Play;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.Server.Settings;
using MineCase.Server.World.Generation;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("chunkColumn")]
    internal class ChunkColumnGrain : AddressByPartitionGrain, IChunkColumn
    {
        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            SetComponent(new StateComponent<StateHolder>());
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        public async Task<BlockState> GetBlockState(int x, int y, int z)
        {
            await EnsureAroundChunkPopulated();
            return State.Storage[x, y, z];
        }

        public async Task<BlockState> GetBlockStateUnsafe(int x, int y, int z)
        {
            await EnsureChunkGenerated();
            return State.Storage[x, y, z];
        }

        public async Task<List<BlockState>> GetBlockStateListUnsafe(List<BlockChunkPos> blockPos)
        {
            await EnsureChunkGenerated();
            List<BlockState> ret = new List<BlockState>();
            foreach (var eachPos in blockPos)
            {
                ret.Add(State.Storage[eachPos.X, eachPos.Y, eachPos.Z]);
            }

            return ret;
        }

        public async Task ApplyChangeUnsafe(List<BlockStateChange> blockChanges)
        {
            await EnsureChunkGenerated();

            foreach (var eachChange in blockChanges)
            {
                var chunkPos = eachChange.Position.ToChunkWorldPos();

                // filter pos not in the chunk
                if (chunkPos != this.GetChunkWorldPos())
                    continue;
                var blockChunkPos = eachChange.Position.ToBlockChunkPos();
                if (eachChange.Condition.Contains(State.Storage[blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z]))
                {
                    State.Storage[blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z] = eachChange.State;
                }
            }
        }

        public async Task<ChunkColumnCompactStorage> GetState()
        {
            await EnsureAroundChunkPopulated();
            return State.Storage;
        }

        public async Task<ChunkColumnCompactStorage> GetStateUnsafe()
        {
            await EnsureChunkGenerated();
            return State.Storage;
        }

        public async Task<BiomeId> GetBlockBiome(int x, int y, int z)
        {
            await EnsureChunkGenerated();
            return (BiomeId)State.Storage.Biomes[((y / 4) * 4 + z / 4) * 4 + x / 4];
        }

        public static readonly (int x, int z)[] CrossCoords = new[]
        {
            (-1, 0), (0, -1), (1, 0), (0, 1)
        };

        public async Task SetBlockState(int x, int y, int z, BlockState blockState)
        {
            await EnsureAroundChunkPopulated();
            var state = State;
            var oldState = state.Storage[x, y, z];

            if (oldState != blockState)
            {
                state.Storage[x, y, z] = blockState;

                var chunkPos = new BlockChunkPos(x, y, z);
                var blockWorldPos = chunkPos.ToBlockWorldPos(ChunkWorldPos);
                await GetBroadcastGenerator().BlockChange(blockWorldPos, blockState);

                if (oldState.Id != blockState.Id)
                {
                    bool replaceOld = true;
                    var newEntity = BlockEntity.Create(GrainFactory, (BlockId)blockState.Id);

                    // 删除旧的 BlockEntity
                    if (state.BlockEntities.TryGetValue(chunkPos, out var entity))
                    {
                        if (object.Equals(entity, newEntity))
                            replaceOld = false;

                        if (replaceOld)
                        {
                            await entity.Tell(DestroyBlockEntity.Default);
                            state.BlockEntities.Remove(chunkPos);
                        }
                    }

                    // 添加新的 BlockEntity
                    if (newEntity != null && replaceOld)
                    {
                        state.BlockEntities.Add(chunkPos, newEntity);
                        await newEntity.Tell(new SpawnBlockEntity { World = World, Position = blockWorldPos });
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
                    return GrainFactory.GetPartitionGrain<IChunkColumn>(World, chunk).OnBlockNeighborChanged(
                        blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z, blockWorldPos, oldState, blockState);
                }));
                MarkDirty();
            }
        }

        public async Task SetBlockStateUnsafe(int x, int y, int z, BlockState blockState)
        {
            await EnsureChunkGenerated();
            var oldState = State.Storage[x, y, z];

            if (oldState != blockState)
            {
                State.Storage[x, y, z] = blockState;
                MarkDirty();
            }
        }

        public async Task SetBlockStateListUnsafe(List<BlockChunkPos> blockPos, BlockState blockState)
        {
            await EnsureChunkGenerated();

            foreach (var eachPos in blockPos)
            {
                var oldState = State.Storage[eachPos.X, eachPos.Y, eachPos.Z];

                if (oldState != blockState)
                {
                    State.Storage[eachPos.X, eachPos.Y, eachPos.Z] = blockState;
                }
            }

            MarkDirty();
        }

        public async Task EnsureChunkGenerated()
        {
            if (!State.Generated)
            {
                var serverSetting = GrainFactory.GetGrain<IServerSettings>(0);
                string worldType = (await serverSetting.GetSettings()).LevelType;
                if (worldType == "DEFAULT" || worldType == "default")
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await World.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                    };
                    State.Storage = await generator.Generate(World, ChunkWorldPos.X, ChunkWorldPos.Z, settings);
                }
                else if (worldType == "FLAT" || worldType == "flat")
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorFlat>(await World.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                        FlatBlockId = new BlockState?[]
                        {
                            BlockStates.Bedrock(),
                            BlockStates.Stone(),
                            BlockStates.Stone(),
                            BlockStates.Dirt(),
                            BlockStates.Dirt(),
                            BlockStates.GrassBlock()
                        }
                    };
                    State.Storage = await generator.Generate(World, ChunkWorldPos.X, ChunkWorldPos.Z, settings);
                }
                else
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await World.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                    };
                    State.Storage = await generator.Generate(World, ChunkWorldPos.X, ChunkWorldPos.Z, settings);
                }

                for (int x = 0; x < 16; ++x)
                {
                    for (int z = 0; z < 16; ++z)
                    {
                        State.Storage.GroundHeight[x, z] = GroundHeight(x, z);
                    }
                }

                State.Generated = true;

                await WriteStateAsync();
            }
        }

        public async Task EnsureChunkPopulated()
        {
            await EnsureChunkGenerated();

            if (!State.Populated)
            {
                var serverSetting = GrainFactory.GetGrain<IServerSettings>(0);
                string worldType = (await serverSetting.GetSettings()).LevelType;
                if (worldType == "DEFAULT" || worldType == "default")
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await World.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings { };
                    await generator.Populate(World, ChunkWorldPos.X, ChunkWorldPos.Z, settings);
                }
                else if (worldType == "FLAT" || worldType == "flat")
                {
                    var generator = GrainFactory.GetGrain<IChunkGeneratorFlat>(await World.GetSeed());
                    GeneratorSettings settings = new GeneratorSettings
                    {
                        FlatBlockId = new BlockState?[]
                        {
                            BlockStates.Bedrock(),
                            BlockStates.Stone(),
                            BlockStates.Stone(),
                            BlockStates.Dirt(),
                            BlockStates.Dirt(),
                            BlockStates.GrassBlock()
                        }
                    };
                    await generator.Populate(World, ChunkWorldPos.X, ChunkWorldPos.Z, settings);
                }
                else
                {
                    throw new System.NotSupportedException("Unknown world type in server setting file.");
                }

                State.Populated = true;

                await WriteStateAsync();
            }
        }

        private async Task EnsureAroundChunkPopulated()
        {
            await EnsureChunkPopulated();

            (var worldKey, var chunkPos) = this.GetWorldAndChunkWorldPos();
            var world = GrainFactory.GetGrain<IWorld>(worldKey);

            int range = 1;
            for (int xOffset = -range; xOffset <= range; ++xOffset)
            {
                for (int zOffset = -range; zOffset <= range; ++zOffset)
                {
                    if (xOffset == 0 && zOffset == 0) continue;
                    var curChunkPos = new ChunkWorldPos(chunkPos.X + xOffset, chunkPos.Z + zOffset);
                    var chunkColumnKey = world.MakeAddressByPartitionKey(curChunkPos);
                    await GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).EnsureChunkPopulated();
                }
            }
        }

        protected ClientPlayPacketGenerator GetBroadcastGenerator()
        {
            return new ClientPlayPacketGenerator(GrainFactory.GetPartitionGrain<IChunkTrackingHub>(World, ChunkWorldPos), null);
        }

        public Task<IBlockEntity> GetBlockEntity(int x, int y, int z)
        {
            if (State.BlockEntities.TryGetValue(new BlockChunkPos(x, y, z), out var entity))
                return Task.FromResult(entity);
            return Task.FromResult<IBlockEntity>(null);
        }

        public Task<int> GetGroundHeight(int x, int z)
        {
            return Task.FromResult(State.Storage.GroundHeight[x, z]);
        }

        public Task OnBlockNeighborChanged(int x, int y, int z, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState)
        {
            if (State.Generated)
            {
                var block = State.Storage[x, y, z];
                var blockHandler = BlockHandler.Create((BlockId)block.Id);
                var selfPosition = new BlockChunkPos(x, y, z).ToBlockWorldPos(ChunkWorldPos);
                return blockHandler.OnNeighborChanged(selfPosition, neighborPosition, oldState, newState, GrainFactory, World);
            }

            return Task.CompletedTask;
        }

        private int GroundHeight(int x, int z)
        {
            var storage = State.Storage;
            for (int y = 255; y >= 0; --y)
            {
                if (!storage[x, y, z].IsAir())
                {
                    return y + 1;
                }
            }

            return 0;
        }

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
        }

        internal class StateHolder
        {
            public bool Populated { get; set; } = false;

            public bool Generated { get; set; } = false;

            public ChunkColumnCompactStorage Storage { get; set; }

            [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
            public Dictionary<BlockChunkPos, IBlockEntity> BlockEntities { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                BlockEntities = new Dictionary<BlockChunkPos, IBlockEntity>();
            }
        }
    }
}