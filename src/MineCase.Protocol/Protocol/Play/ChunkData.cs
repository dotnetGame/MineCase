using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt;
using MineCase.Serialization;
using MineCase.World;

namespace MineCase.Protocol.Play
{
    [Packet(0x20)]
    public sealed class ChunkData : ISerializablePacket
    {
        [SerializeAs(DataType.Int)]
        public int ChunkX;

        [SerializeAs(DataType.Int)]
        public int ChunkZ;

        [SerializeAs(DataType.Boolean)]
        public bool FullChunk;

        [SerializeAs(DataType.VarInt)]
        public uint PrimaryBitMask;

        [SerializeAs(DataType.ByteArray)]
        public long[] Heightmaps;

        [SerializeAs(DataType.VarInt)]
        public uint Size;

        [SerializeAs(DataType.Array)]
        public ChunkSection[] Data;

        [SerializeAs(DataType.Array)]
        public int[] Biomes;

        [SerializeAs(DataType.VarInt)]
        public uint NumberOfBlockEntities;

        [SerializeAs(DataType.Array)]
        public Nbt.Tags.NbtList BlockEntities;

        public ChunkData()
        {
            Heightmaps = new long[16 * 16];
            BlockEntities = new Nbt.Tags.NbtList(Nbt.NbtTagType.Compound);
        }

        public static ChunkData Deserialize(ref SpanReader br, bool isOverworld)
        {
            var result = new ChunkData
            {
                ChunkX = br.ReadAsInt(),
                ChunkZ = br.ReadAsInt(),
                FullChunk = br.ReadAsBoolean(),
                PrimaryBitMask = br.ReadAsVarInt(out _)
            };

            /*
            var hasBioms = result.FullChunk;

            result.Size = br.ReadAsVarInt(out _);
            var dataReader = br.ReadAsSubReader((int)result.Size - (hasBioms ? 256 : 0));
            var data = new List<ChunkSection>();
            while (!dataReader.IsCosumed)
                data.Add(ChunkSection.Deserialize(ref dataReader, isOverworld));
            result.Data = data.ToArray();

            if (hasBioms)
                result.Biomes = br.ReadAsByteArray(256);
            result.NumberOfBlockEntities = br.ReadAsVarInt(out _);
            */
            return result;
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(ChunkX);
            bw.WriteAsInt(ChunkZ);
            bw.WriteAsBoolean(FullChunk);
            bw.WriteAsVarInt(PrimaryBitMask, out _);

            var heightmaps = new Nbt.Tags.NbtCompound();
            heightmaps.Add(new Nbt.Tags.NbtLongArray(Heightmaps, "MOTION_BLOCKING"));

            Nbt.Serialization.NbtTagSerializer.SerializeTag(heightmaps, bw);

            using (var mem = new MemoryStream())
            {
                using (var dbw = new BinaryWriter(mem, Encoding.UTF8, true))
                    dbw.WriteAsArray(Data);

                var dataBytes = mem.ToArray();
                Size = (uint)dataBytes.Length;

                bw.WriteAsVarInt(Size, out _);
                bw.WriteAsByteArray(dataBytes);
            }

            if (Biomes != null)
            {
                foreach (var eachBiome in Biomes)
                    bw.WriteAsInt(eachBiome);
            }

            bw.WriteAsVarInt(NumberOfBlockEntities, out _);

            Nbt.Serialization.NbtTagSerializer.SerializeTag(BlockEntities, bw);
        }
    }

    public sealed class ChunkSection : ISerializablePacket
    {
        [SerializeAs(DataType.Short)]
        public short BlockCount;

        [SerializeAs(DataType.Byte)]
        public byte BitsPerBlock;

        [SerializeAs(DataType.Int)]
        public uint PaletteLength;

        [SerializeAs(DataType.Array)]
        public byte[] Palette;

        [SerializeAs(DataType.VarInt)]
        public uint DataArrayLength;

        [SerializeAs(DataType.Array)]
        public long[] DataArray;

        public static ChunkSection Deserialize(ref SpanReader br, bool isOverworld)
        {
            var result = new ChunkSection
            {
                BitsPerBlock = br.ReadAsByte(),
                PaletteLength = br.ReadAsVarInt(out _)
            };
            /*
            if (result.PaletteLength != 0)
            {
                var paletteReader = br.ReadAsSubReader((int)result.PaletteLength);
                var palette = new List<uint>();
                while (!paletteReader.IsCosumed)
                    palette.Add(paletteReader.ReadAsVarInt(out _));
                result.Palette = palette.ToArray();
            }

            result.DataArrayLength = br.ReadAsVarInt(out _);
            var dataArray = new ulong[result.DataArrayLength];
            for (int i = 0; i < dataArray.Length; i++)
                dataArray[i] = br.ReadAsUnsignedLong();
            result.DataArray = dataArray;
            result.BlockLight = br.ReadAsByteArray(ChunkConstants.BlocksInSection / 2);
            if (isOverworld)
                result.SkyLight = br.ReadAsByteArray(ChunkConstants.BlocksInSection / 2);
                */
            return result;
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsShort(BlockCount);
            bw.WriteAsUnsignedByte(BitsPerBlock);

            byte realBitsPerBlock = 0;
            if (BitsPerBlock <= 4)
            {
                realBitsPerBlock = 4;
            }
            else if (BitsPerBlock >= 5 && BitsPerBlock <= 8)
            {
                realBitsPerBlock = BitsPerBlock;
            }
            else
            {
                realBitsPerBlock = BitsPerBlock;
            }

            bw.WriteAsVarInt(PaletteLength, out _);
            foreach (var paletteElement in Palette)
            {
                bw.WriteAsVarInt(paletteElement, out _);
            }

            bw.WriteAsVarInt(DataArrayLength, out _);
            foreach (var eachData in DataArray)
            {
                bw.WriteAsLong(eachData);
            }

        }
    }
}
