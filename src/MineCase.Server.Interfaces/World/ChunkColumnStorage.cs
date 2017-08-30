using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MineCase.Server.World
{
    public static class ChunkConstants
    {
        public const int SectionsPerChunk = 16;

        public const int BlockEdgeWidthInSection = 16;

        public const int BlocksInSection = BlockEdgeWidthInSection * BlockEdgeWidthInSection * BlockEdgeWidthInSection;
    }

    public sealed class ChunkColumnStorage
    {
        public uint SectionBitMask
        {
            get
            {
                uint mask = 0;
                int index = 0;
                while (index < ChunkConstants.SectionsPerChunk)
                {
                    mask <<= 1;
                    mask |= Sections[index++] != null ? 1u : 0;
                }

                return mask;
            }
        }

        public ChunkSectionStorage[] Sections { get; } = new ChunkSectionStorage[ChunkConstants.SectionsPerChunk];

        public byte[] Biomes { get; } = new byte[256];

        public BlockState this[int x, int y, int z]
        {
            get => Sections[y / 16].Data[x, y % 16, z];
            set => Sections[y / 16].Data[x, y % 16, z] = value;
        }
    }

    public sealed class ChunkSectionStorage
    {
        private const byte _bitsId = 9;
        private const byte _bitsMeta = 4;
        private const byte _bitsPerBlock = _bitsId + _bitsMeta;
        private const uint _idMask = (1u << _bitsId) - 1;
        private const uint _metaMask = (1u << _bitsMeta) - 1;
        private const ulong _blockMask = (1u << _bitsPerBlock) - 1;

        public byte BitsPerBlock => _bitsPerBlock;

        public DataArray Data { get; } = new DataArray();

        public NibbleArray BlockLight { get; } = new NibbleArray();

        public NibbleArray SkyLight { get; }

        public ChunkSectionStorage(bool hasSkylight)
        {
            if (hasSkylight)
                SkyLight = new NibbleArray();
        }

        public sealed class DataArray
        {
            public ulong[] Storage { get; } = new ulong[ChunkConstants.BlocksInSection * _bitsPerBlock / 64];

            public BlockState this[int x, int y, int z]
            {
                get
                {
                    var offset = GetOffset(x, y, z);
                    var toRead = Math.Min(_bitsPerBlock, 64 - offset.bitOffset);
                    var value = Storage[offset.indexOffset] >> offset.bitOffset;
                    var rest = _bitsPerBlock - toRead;
                    if (rest > 0)
                        value = (value << rest) | (Storage[offset.bitOffset + 1] & ((1u << rest) - 1));
                    return new BlockState { Id = (uint)((value >> _bitsMeta) & _idMask), MetaValue = (uint)(value & _metaMask) };
                }

                set
                {
                    var stgValue = ((ulong)value.Id << _bitsMeta) | (value.MetaValue & _metaMask);
                    var offset = GetOffset(x, y, z);
                    var tmpValue = Storage[offset.indexOffset];
                    var mask = _blockMask << offset.bitOffset;
                    var toWrite = Math.Min(_bitsPerBlock, 64 - offset.bitOffset);
                    Storage[offset.indexOffset] = (tmpValue & ~mask) | (stgValue << offset.bitOffset);
                    var rest = _bitsPerBlock - toWrite;
                    if (rest > 0)
                    {
                        mask = (1u << rest) - 1;
                        tmpValue = Storage[offset.indexOffset + 1];
                        stgValue >>= toWrite;
                        Storage[offset.indexOffset + 1] = (tmpValue & ~mask) | (stgValue & mask);
                    }
                }
            }

            private static (int indexOffset, int bitOffset) GetOffset(int x, int y, int z)
            {
                var index = GetBlockSerialIndex(x, y, z) * _bitsPerBlock;
                return (index / 64, index % 64);
            }
        }

        public sealed class NibbleArray
        {
            public byte[] Storage { get; } = new byte[ChunkConstants.BlocksInSection / 2];

            public byte this[int x, int y, int z]
            {
                get
                {
                    var offset = GetBlockSerialIndex(x, y, z);
                    var value = Storage[offset / 2];
                    return offset % 2 == 0 ? (byte)(value >> 0xF) : (byte)(value & 0xF);
                }

                set
                {
                    var offset = GetBlockSerialIndex(x, y, z);
                    var tmpValue = Storage[offset / 2];
                    if (offset % 2 == 0)
                        Storage[offset / 2] = (byte)((tmpValue & 0xF) | (value << 4));
                    else
                        Storage[offset / 2] = (byte)((tmpValue & 0xF0) | (value & 0xF));
                }
            }
        }

        private static int GetBlockSerialIndex(int x, int y, int z)
        {
            return ((y * ChunkConstants.BlockEdgeWidthInSection) + z) * ChunkConstants.BlockEdgeWidthInSection + x;
        }
    }
}
