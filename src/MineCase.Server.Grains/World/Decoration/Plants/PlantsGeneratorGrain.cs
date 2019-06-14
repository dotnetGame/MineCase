using System;
using System.Threading.Tasks;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.Server.World.Decoration;
using MineCase.World;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [JsonObject(MemberSerialization.OptOut)]
    public class PlantsInfo
    {
        [JsonProperty(PropertyName = "TreeHeight")]
        public int TreeHeight { get; set; } = 5;

        [JsonProperty(PropertyName = "TreeVine")]
        public bool TreeVine { get; set; } = false;

        [JsonProperty(PropertyName = "PlantType")]
        public PlantsType PlantType { get; set; } = PlantsType.Oak;
    }

    public abstract class PlantsGeneratorGrain : Grain, IPlantsGenerator
    {
        protected virtual Task SetBlock(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState state)
        {
            var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
            var chunkGrain = GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey);

            BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
            return chunkGrain.SetBlockStateUnsafe(blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z, state);
        }

        protected virtual Task<BlockState> GetBlock(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
            var chunkGrain = GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey);
            BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();

            return chunkGrain.GetBlockStateUnsafe(blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z);
        }

        protected async virtual Task SetIfAir(IWorld world, ChunkWorldPos chunkWorldPos, int x, int y, int z, BlockState block)
        {
            BlockState state = await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z));
            if (state.IsAir())
            {
                await SetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z), block);
            }
        }

        protected async virtual Task SetIfAir(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState block)
        {
            BlockState state = await GetBlock(world, chunkWorldPos, pos);
            if (state.IsAir())
            {
                await SetBlock(world, chunkWorldPos, pos, block);
            }
        }

        protected async virtual Task RandomSetIfAir(IWorld world, ChunkWorldPos chunkWorldPos, int x, int y, int z, BlockState block, Random rand, float freq)
        {
            BlockState state = await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z));
            if (rand.NextDouble() < freq && state.IsAir())
            {
                await SetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z), block);
            }
        }

        protected async virtual Task RandomSetIfAir(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState block, Random rand, float freq)
        {
            BlockState state = await GetBlock(world, chunkWorldPos, pos);
            if (rand.NextDouble() < freq && state.IsAir())
            {
                await SetBlock(world, chunkWorldPos, pos, block);
            }
        }

        private Task<int> GetGroundHeight(IWorld world, ChunkWorldPos chunkWorldPos, int x, int z)
        {
            var blockChunkPos = new BlockWorldPos { X = x, Z = z }.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(chunkWorldPos);
            return GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetGroundHeight(blockChunkPos.X, blockChunkPos.Z);
        }

        public abstract Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);

        public virtual async Task Generate(IWorld world, ChunkWorldPos pos, int countPerChunk)
        {
            int seed = await world.GetSeed();

            BlockWorldPos curChunkCorner = pos.ToBlockWorldPos();

            int chunkSeed = pos.X ^ pos.Z ^ seed ^ this.GetType().GetHashCode() ^ this.GetPrimaryKeyString().GetHashCode();
            Random rand = new Random(chunkSeed);
            int countCurChunk = rand.Next(countPerChunk + 1);

            for (int count = 0; count < countCurChunk; ++count)
            {
                int x = curChunkCorner.X + rand.Next(16);
                int z = curChunkCorner.Z + rand.Next(16);
                int groundHeight = await GetGroundHeight(world, pos, x, z);
                await GenerateSingle(world, pos, new BlockWorldPos { X = x, Y = groundHeight, Z = z });
            }
        }
    }
}
