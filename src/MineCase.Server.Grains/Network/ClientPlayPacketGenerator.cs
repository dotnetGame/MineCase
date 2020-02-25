using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Play.Client;
using MineCase.World.Chunk;
using Orleans;

namespace MineCase.Server.Network
{
    internal class ClientPlayPacketGenerator
    {
        private IGrainFactory _grainFactory;

        public ClientPlayPacketGenerator(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        public async Task<ChunkData> ChunkData(int chunkX, int chunkZ, ChunkColumn chunk, int blockChangeMask)
        {
            bool fullChunk = blockChangeMask == 0xFFFF;
            var chunkData = new ChunkData
            {
                ChunkX = chunkX,
                ChunkZ = chunkZ,
                FullChunk = fullChunk,
                PrimaryBitMask = GetPrimaryBitMask(chunk, fullChunk, blockChangeMask),
                Heightmaps = new Nbt.Tags.NbtCompound(),
                Biomes = null,
                Data = new byte[GetDataSize(chunk, fullChunk, blockChangeMask)],
                NumberOfBlockEntities = 0,
                BlockEntities = new List<Nbt.Tags.NbtCompound>(),
            };

            if (chunkData.FullChunk)
            {
                chunkData.Biomes = chunk.Biomes;
            }

            return chunkData;
        }

        protected int GetDataSize(ChunkColumn chunk, bool isFullChunk, int blockChangeMask)
        {
            int size = 0;
            ChunkSection[] sections = chunk.Sections;

            for (int i = 0; i < sections.Length; ++i)
            {
                ChunkSection chunksection = sections[i];
                if (chunksection != ChunkColumn.EmptySection && (!isFullChunk || !chunksection.Empty) && (blockChangeMask & 1 << i) != 0)
                {
                    size += chunksection.GetSerializedSize();
                }
            }

            return size;
        }

        public int GetPrimaryBitMask(ChunkColumn chunk, bool isFullChunk, int blockChangeMask)
        {
            int mask = 0;
            ChunkSection[] sections = chunk.Sections;

            for (int i = 0; i < sections.Length; ++i)
            {
                ChunkSection chunksection = sections[i];
                if (chunksection != ChunkColumn.EmptySection && (!isFullChunk || !chunksection.Empty) && (blockChangeMask & 1 << i) != 0)
                {
                    mask |= 1 << i;
                }
            }

            return mask;
        }
    }
}
