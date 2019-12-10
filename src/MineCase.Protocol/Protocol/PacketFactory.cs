using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Core.World;
using MineCase.Protocol.Play;
using MineCase.World;

namespace MineCase.Protocol
{
    public class PacketFactory
    {
        protected static int GetChunkDataSize(ChunkData packet,Chunk chunkIn, int p_218709_2_)
        {
            int i = 0;
            ChunkSection[] achunksection = chunkIn.Sections;
            int j = 0;

            for (int k = achunksection.Length; j < k; ++j)
            {
                ChunkSection chunksection = achunksection[j];
                if (chunksection != ChunkSection.EmptySection && (!packet.FullChunk || !chunksection.IsEmpty()) && (p_218709_2_ & 1 << j) != 0)
                {
                    i += chunksection.GetSize();
                }
            }

            if (packet.FullChunk)
            {
                i += chunkIn.GetBiomes().Length * 4;
            }

            return i;
        }

        public static ChunkData ChunkDataPacket(Chunk chunkIn, int changedSectionFilter)
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

            ret.Data = new byte[GetChunkDataSize(chunkIn, changedSectionFilter)];
            ret.PrimaryBitMask = this.func_218708_a(new PacketBuffer(this.getWriteBuffer()), chunkIn, changedSectionFilter);
            ret.BlockEntities = Lists.newArrayList();

            for (Entry<BlockPos, TileEntity> entry1 : chunkIn.getTileEntityMap().entrySet())
            {
                BlockPos blockpos = entry1.getKey();
                TileEntity tileentity = entry1.getValue();
                int i = blockpos.getY() >> 4;
                if (this.isFullChunk() || (changedSectionFilter & 1 << i) != 0)
                {
                    CompoundNBT compoundnbt = tileentity.getUpdateTag();
                    this.tileEntityTags.add(compoundnbt);
                }
            }

            return ret;
        }
    }
}
