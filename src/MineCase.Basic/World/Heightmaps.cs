using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World
{
    public class Heightmap
    {

        /*
        private Predicate<BlockState> field_222693_d;
        */
        private int[] _data = new int[256];

        // private ChunkColumn _chunk;

        public int GetHeight(int x, int z)
        {
            return GetHeight(GetDataArrayIndex(x, z));
        }

        private int GetHeight(int dataArrayIndex)
        {
            return _data[dataArrayIndex];
        }

        private void Set(int x, int z, int value)
        {
            _data[GetDataArrayIndex(x, z)] = value;
        }

        public void SetDataArray(int[] dataIn)
        {
            dataIn.CopyTo(_data, 0);
        }

        public long[] GetDataArray()
        {
            long[] ret = new long[256];
            _data.CopyTo(ret, 0);
            return ret;
        }

        private static int GetDataArrayIndex(int x, int z)
        {
            return x + z * 16;
        }
    }

    public struct HeightmapType
    {
        public string ID { get; set; }

        public Heightmaps.Usage Usage { get; set; }
    }

    public class Heightmaps
    {
        public static HeightmapType GetHeightmapType(Heightmaps.Type t)
        {
            if (t == Type.WORLD_SURFACE_WG)
            {
                return new HeightmapType { ID = "WORLD_SURFACE_WG", Usage = Usage.WORLDGEN };
            }
            else if (t == Type.WORLD_SURFACE)
            {
                return new HeightmapType { ID = "WORLD_SURFACE", Usage = Usage.CLIENT };
            }
            else if (t == Type.OCEAN_FLOOR_WG)
            {
                return new HeightmapType { ID = "OCEAN_FLOOR_WG", Usage = Usage.WORLDGEN };
            }
            else if (t == Type.OCEAN_FLOOR)
            {
                return new HeightmapType { ID = "OCEAN_FLOOR", Usage = Usage.LIVE_WORLD };
            }
            else if (t == Type.MOTION_BLOCKING)
            {
                return new HeightmapType { ID = "MOTION_BLOCKING", Usage = Usage.CLIENT };
            }
            else if (t == Type.MOTION_BLOCKING_NO_LEAVES)
            {
                return new HeightmapType { ID = "MOTION_BLOCKING_NO_LEAVES", Usage = Usage.LIVE_WORLD };
            }
            else
            {
                return new HeightmapType { ID = "WORLD_SURFACE_WG", Usage = Usage.WORLDGEN };
            }
        }

        public enum Type
        {
            WORLD_SURFACE_WG,
            WORLD_SURFACE,
            OCEAN_FLOOR_WG,
            OCEAN_FLOOR,
            MOTION_BLOCKING,
            MOTION_BLOCKING_NO_LEAVES
        }

        public enum Usage
        {
            WORLDGEN,
            LIVE_WORLD,
            CLIENT
        }
    }
}
