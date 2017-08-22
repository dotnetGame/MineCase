using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            bw.WriteAsVarInt(NumberOfBlockEntities, out _);
        }
    }

    public sealed class ChunkSection : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte BitPerBlock;

        [SerializeAs(DataType.VarInt)]
        public uint PaletteLength;

        [SerializeAs(DataType.Array)]
        public uint[] Palette;

        [SerializeAs(DataType.VarInt)]
        public uint DataArrayLength;

        [SerializeAs(DataType.Array)]
        public long[] DataArray;

        [SerializeAs(DataType.Array)]
        public byte[] BlockLight;

        [SerializeAs(DataType.Array)]
        public byte[] SkyLight;

        public void Serialize(BinaryWriter bw)
        {
            DataArrayLength = (uint)DataArray.Length;
            bw.WriteAsByte(BitPerBlock);

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
                bw.WriteAsLong(item);
            bw.WriteAsByteArray(BlockLight);
            if (SkyLight != null)
                bw.WriteAsByteArray(SkyLight);
        }
    }
}
