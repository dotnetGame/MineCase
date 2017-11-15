using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using MineCase.World;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x20)]
    public sealed class ChunkData : ISerializablePacket
    {
        [SerializeAs(DataType.Int)]
        public int ChunkX;

        [SerializeAs(DataType.Int)]
        public int ChunkZ;

        [SerializeAs(DataType.Boolean)]
        public bool GroundUpContinuous;

        [SerializeAs(DataType.VarInt)]
        public uint PrimaryBitMask;

        [SerializeAs(DataType.VarInt)]
        public uint Size;

        [SerializeAs(DataType.Array)]
        public ChunkSection[] Data;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Biomes;

        [SerializeAs(DataType.VarInt)]
        public uint NumberOfBlockEntities;

        public static ChunkData Deserialize(ref SpanReader br, bool isOverworld)
        {
            var result = new ChunkData
            {
                ChunkX = br.ReadAsInt(),
                ChunkZ = br.ReadAsInt(),
                GroundUpContinuous = br.ReadAsBoolean(),
                PrimaryBitMask = br.ReadAsVarInt(out _)
            };

            var hasBioms = result.GroundUpContinuous;

            result.Size = br.ReadAsVarInt(out _);
            var dataReader = br.ReadAsSubReader((int)result.Size - (hasBioms ? 256 : 0));
            var data = new List<ChunkSection>();
            while (!dataReader.IsCosumed)
                data.Add(ChunkSection.Deserialize(ref dataReader, isOverworld));
            result.Data = data.ToArray();

            if (hasBioms)
                result.Biomes = br.ReadAsByteArray(256);
            result.NumberOfBlockEntities = br.ReadAsVarInt(out _);
            return result;
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(ChunkX);
            bw.WriteAsInt(ChunkZ);
            bw.WriteAsBoolean(GroundUpContinuous);
            bw.WriteAsVarInt(PrimaryBitMask, out _);

            using (var mem = new MemoryStream())
            {
                using (var dbw = new BinaryWriter(mem, Encoding.UTF8, true))
                    dbw.WriteAsArray(Data);

                var dataBytes = mem.ToArray();
                Size = (uint)(dataBytes.Length + (Biomes?.Length ?? 0));

                bw.WriteAsVarInt(Size, out _);
                bw.WriteAsByteArray(dataBytes);
            }

            if (Biomes != null)
                bw.WriteAsByteArray(Biomes);

            bw.WriteAsVarInt(NumberOfBlockEntities, out _);
        }
    }

#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    public sealed class ChunkSection : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte BitsPerBlock;

        [SerializeAs(DataType.VarInt)]
        public uint PaletteLength;

        [SerializeAs(DataType.Array)]
        public uint[] Palette;

        [SerializeAs(DataType.VarInt)]
        public uint DataArrayLength;

        [SerializeAs(DataType.Array)]
        public ulong[] DataArray;

        [SerializeAs(DataType.Array)]
        public byte[] BlockLight;

        [SerializeAs(DataType.Array)]
        public byte[] SkyLight;

        public static ChunkSection Deserialize(ref SpanReader br, bool isOverworld)
        {
            var result = new ChunkSection
            {
                BitsPerBlock = br.ReadAsByte(),
                PaletteLength = br.ReadAsVarInt(out _)
            };

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
            return result;
        }

        public void Serialize(BinaryWriter bw)
        {
            DataArrayLength = (uint)DataArray.Length;
            bw.WriteAsByte(BitsPerBlock);

            if (Palette != null)
            {
                using (var mem = new MemoryStream())
                {
                    using (var pbw = new BinaryWriter(mem, Encoding.UTF8, true))
                    {
                        foreach (var item in Palette)
                            pbw.WriteAsVarInt(item, out _);
                    }

                    var paletteBytes = mem.ToArray();
                    PaletteLength = (uint)paletteBytes.Length;
                    bw.WriteAsVarInt(PaletteLength, out _);
                    bw.WriteAsByteArray(paletteBytes);
                }
            }
            else
            {
                PaletteLength = 0;
                bw.WriteAsVarInt(PaletteLength, out _);
            }

            bw.WriteAsVarInt(DataArrayLength, out _);
            foreach (var item in DataArray)
                bw.WriteAsUnsignedLong(item);
            bw.WriteAsByteArray(BlockLight);
            if (SkyLight != null)
                bw.WriteAsByteArray(SkyLight);
        }
    }
}
