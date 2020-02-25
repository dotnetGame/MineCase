using MineCase.Block;
using MineCase.Network;
using MineCase.Util.Palette;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.World.Chunk
{
    public class ChunkSection
    {
        public PalettedData<BlockState> Data { get; set; }

        public int NonAirBlockCount { get; set; }

        public bool Empty { get => Data == null; }
        public ChunkSection()
        {
            Data = new PalettedData<BlockState>(Blocks.GlobalPalette, Blocks.BlockStateRegistry, ChunkConstants.BlocksInSection, Blocks.Air.Default);
            NonAirBlockCount = 0;
        }

        public ChunkSection(PalettedData<BlockState> data, int nonAirBlockCount)
        {
            Data = data;
            NonAirBlockCount = nonAirBlockCount;
        }

        public BlockState this[int x, int y, int z]
        {
            get
            {
                return Data[GetIndex(x, y, z)];
            }
            set
            {
                BlockState originState = Data[GetIndex(x, y, z)];
                if (!originState.IsAir())
                    --NonAirBlockCount;
                if (!value.IsAir())
                    ++NonAirBlockCount;
                Data[GetIndex(x, y, z)] = value;
            }
        }

        public ulong[] GetDataArray()
        {
            return Data.GetStorage();
        }

        public void Read(BinaryReader br)
        {
            NonAirBlockCount = br.ReadAsShort();
            Data.Read(br);
        }

        public int GetSerializedSize()
        {
            return 2 + Data.GetSerializedSize();
        }

        private static int GetIndex(int x, int y, int z)
        {
            return y << 8 | z << 4 | x;
        }
    }
}
