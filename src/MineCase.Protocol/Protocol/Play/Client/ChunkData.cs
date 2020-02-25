using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using MineCase.World.Biome;
using MineCase.World.Chunk;

namespace MineCase.Protocol.Play.Client
{
    [Packet(0x22, ProtocolType.Play, PacketDirection.ClientBound)]
    public sealed class ChunkData : ISerializablePacket
    {
        [SerializeAs(DataType.Int)]
        public int ChunkX;

        [SerializeAs(DataType.Int)]
        public int ChunkZ;

        [SerializeAs(DataType.Boolean)]
        public bool FullChunk;

        [SerializeAs(DataType.VarInt)]
        public int PrimaryBitMask;

        [SerializeAs(DataType.ByteArray)]
        public Nbt.Tags.NbtCompound Heightmaps;

        [SerializeAs(DataType.Array)]
        public BiomeId[] Biomes;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;

        [SerializeAs(DataType.VarInt)]
        public uint NumberOfBlockEntities;

        [SerializeAs(DataType.Array)]
        public List<Nbt.Tags.NbtCompound> BlockEntities;

        public void Deserialize(BinaryReader br)
        {
            ChunkX = br.ReadAsInt();
            ChunkZ = br.ReadAsInt();
            FullChunk = br.ReadAsBoolean();
            PrimaryBitMask = br.ReadAsVarInt(out _);
            Heightmaps = br.ReadAsCompoundTag();
            if (FullChunk)
            {
                Biomes = new BiomeId[BiomeConstants.ChunkBiomeNum];
                for (int i = 0; i < Biomes.Length; ++i)
                {
                    Biomes[i] = (BiomeId)br.ReadAsInt();
                }
            }

            int size = br.ReadAsVarInt(out _);
            if (size > 2 * 1024 * 1024)
                throw new InvalidDataException("Chunk Packet trying to allocate too much memory on read.");
            Data = br.ReadAsByteArray(size);
            int blockEntitiesNum = br.ReadAsVarInt(out _);
            BlockEntities = new List<Nbt.Tags.NbtCompound>();

            for (int i = 0; i < blockEntitiesNum; ++i)
            {
                BlockEntities.Add(br.ReadAsCompoundTag());
            }
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(ChunkX);
            bw.WriteAsInt(ChunkZ);
            bw.WriteAsBoolean(FullChunk);
            bw.WriteAsVarInt(PrimaryBitMask, out _);
            bw.WriteAsCompoundTag(Heightmaps);
            if (Biomes != null)
            {
                foreach (BiomeId id in Biomes)
                {
                    bw.WriteAsInt((int)id);
                }
            }

            bw.WriteAsVarInt(Data.Length, out _);
            bw.WriteAsByteArray(Data);
            bw.WriteAsVarInt(BlockEntities.Count, out _);

            foreach (Nbt.Tags.NbtCompound compoundnbt in BlockEntities)
            {
                bw.WriteAsCompoundTag(compoundnbt);
            }
        }
    }
}
