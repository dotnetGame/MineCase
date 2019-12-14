using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.World;
using MineCase.World.Biome;
using MineCase.World.Chunk;

namespace MineCase.Protocol
{
    public class PacketFactory
    {
        protected static int GetChunkDataSize(ChunkData packet, ChunkColumn chunkIn, int sectionBitmask)
        {
            int i = 0;
            ChunkSection[] achunksection = chunkIn.Sections;
            int y = 0;

            for (int k = achunksection.Length; y < k; ++y)
            {
                ChunkSection chunksection = achunksection[y];
                if (chunksection != ChunkSection.EmptySection && (!packet.FullChunk || !chunksection.IsEmpty()) && (sectionBitmask & 1 << y) != 0)
                {
                    // i += chunksection.GetSize();
                }
            }

            if (packet.FullChunk)
            {
                i += chunkIn.BlockBiomeArray.Length * 4;
            }

            return i;
        }


        public static int WriteAndGetPrimaryBitMask(ChunkData packet, BinaryWriter bw, ChunkColumn chunkIn, int sectionBitmask)
        {
            int i = 0;
            ChunkSection[] achunksection = chunkIn.Sections;
            int j = 0;

            for (int k = achunksection.Length; j < k; ++j)
            {
                ChunkSection chunksection = achunksection[j];
                if (chunksection != ChunkSection.EmptySection && (!packet.FullChunk || !chunksection.IsEmpty()) && (sectionBitmask & 1 << j) != 0)
                {
                    i |= 1 << j;
                    bw.WriteAsChunkSection(chunksection);
                }
            }

            if (packet.FullChunk)
            {
                Biome[] abiome = chunkIn.BlockBiomeArray;

                for (int l = 0; l < abiome.Length; ++l)
                {
                    bw.WriteAsInt((int)abiome[l].GetBiomeId());
                }
            }

            return i;
        }

        public static ChunkData ChunkDataPacket(ChunkColumn chunkIn, int sectionBitmask)
        {
            ChunkData ret = new ChunkData { };
            ChunkWorldPos chunkpos = chunkIn.Posistion;
            ret.ChunkX = chunkpos.X;
            ret.ChunkZ = chunkpos.Z;
            ret.FullChunk = sectionBitmask == 65535;
            ret.Heightmaps = new Nbt.Tags.NbtCompound();

            foreach (var entry in chunkIn.HeightMaps)
            {
                var entryKey = Heightmaps.GetHeightmapType(entry.Key);
                if (entryKey.Usage == Heightmaps.Usage.CLIENT)
                {
                    ret.Heightmaps.Add(entryKey.ID, new Nbt.Tags.NbtLongArray(entry.Value.GetDataArray()));
                }
            }

            using (var stream = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(stream))
                {
                    ret.PrimaryBitMask = (uint)WriteAndGetPrimaryBitMask(ret, bw, chunkIn, sectionBitmask);
                    ret.Data = stream.ToArray();
                }
            }

            ret.BlockEntities = new List<Nbt.Tags.NbtCompound>();

            foreach (var entry in chunkIn.BlockEntities)
            {
                BlockWorldPos blockpos = entry.Key;
                BlockEntity.BlockEntity blockEntity = entry.Value;
                int i = blockpos.Y >> 4;
                if (ret.FullChunk || (sectionBitmask & 1 << i) != 0)
                {
                    // Nbt.Tags.NbtCompound compoundnbt = blockEntity.getUpdateTag();
                    // ret.BlockEntities.Add(compoundnbt);
                }
            }

            return ret;
        }
    }
}
