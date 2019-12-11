using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol.Play;
using MineCase.World;
using MineCase.World.Biome;

namespace MineCase.Protocol
{
    public class PacketFactory
    {
        protected static int GetChunkDataSize(ChunkData packet, ChunkColumn chunkIn, int changedSectionFilter)
        {
            int i = 0;
            ChunkSection[] achunksection = chunkIn.Sections;
            int j = 0;

            for (int k = achunksection.Length; j < k; ++j)
            {
                ChunkSection chunksection = achunksection[j];
                if (chunksection != ChunkSection.EmptySection && (!packet.FullChunk || !chunksection.IsEmpty()) && (changedSectionFilter & 1 << j) != 0)
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

        /*
        public static int WriteAndGetPrimaryBitMask(ChunkData packet, PacketBuffer p_218708_1_, ChunkColumn chunkIn, int changedSectionFilter)
        {
            int i = 0;
            ChunkSection[] achunksection = chunkIn.Sections;
            int j = 0;

            for (int k = achunksection.Length; j < k; ++j)
            {
                ChunkSection chunksection = achunksection[j];
                if (chunksection != ChunkSection.EmptySection && (!packet.FullChunk || !chunksection.IsEmpty()) && (changedSectionFilter & 1 << j) != 0)
                {
                    i |= 1 << j;
                    chunksection.write(p_218708_1_);
                }
            }

            if (packet.FullChunk)
            {
                Biome[] abiome = chunkIn.BlockBiomeArray;

                for (int l = 0; l < abiome.Length; ++l)
                {
                    p_218708_1_.writeInt(abiome[l].Properties.BiomeId);
                }
            }

            return i;
        }
        */

        public static ChunkData ChunkDataPacket(ChunkColumn chunkIn, int changedSectionFilter)
        {
            ChunkData ret = new ChunkData { };
            ChunkWorldPos chunkpos = chunkIn.Posistion;
            ret.ChunkX = chunkpos.X;
            ret.ChunkZ = chunkpos.Z;
            ret.FullChunk = changedSectionFilter == 65535;
            ret.Heightmaps = new Nbt.Tags.NbtCompound();

            foreach (var entry in chunkIn.HeightMaps)
            {
                var entryKey = Heightmaps.GetHeightmapType(entry.Key);
                if (entryKey.Usage == Heightmaps.Usage.CLIENT)
                {
                    ret.Heightmaps.Add(new Nbt.Tags.NbtLongArray(entry.Value.GetDataArray(), entryKey.ID));
                }
            }

            ret.Data = new byte[GetChunkDataSize(ret, chunkIn, changedSectionFilter)];

            // ret.PrimaryBitMask = WriteAndGetPrimaryBitMask(new PacketBuffer(this.getWriteBuffer()), chunkIn, changedSectionFilter);
            ret.BlockEntities = new List<Nbt.Tags.NbtCompound>();

            foreach (var entry in chunkIn.BlockEntities)
            {
                BlockWorldPos blockpos = entry.Key;
                BlockEntity.BlockEntity blockEntity = entry.Value;
                int i = blockpos.Y >> 4;
                if (ret.FullChunk || (changedSectionFilter & 1 << i) != 0)
                {
                    // Nbt.Tags.NbtCompound compoundnbt = blockEntity.getUpdateTag();
                    // ret.BlockEntities.Add(compoundnbt);
                }
            }

            return ret;
        }
    }
}
