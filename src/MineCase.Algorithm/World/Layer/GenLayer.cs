using System;

namespace MineCase.Algorithm.World.Layer
{
    public abstract class GenLayer
    {
        protected int _baseSeed;

        protected GenLayer _parent;

        public GenLayer(int seed, GenLayer parent)
        {
            _baseSeed = seed;
            _parent = parent;
        }

        public abstract int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight);

        public static GenLayer InitAllLayer(int seed)
        {
            GenLayer addIsland0 = new GenLayerIsland(seed, null);
            GenLayer zoomed0 = new GenLayerZoom(seed, addIsland0);

            GenLayer biomesAdded = new GenLayerBiome(seed, zoomed0);
            GenLayer addIsland1 = new GenLayerAddIsland(2, biomesAdded);
            GenLayer zoomed1 = new GenLayerZoom(seed, addIsland1);
            GenLayer addIsland2 = new GenLayerAddIsland(50, zoomed1);

            // GenLayer zoomed2 = new GenLayerZoom(seed, zoomed0);
            GenLayer zoomed2 = GenLayerZoom.Magnify(seed, addIsland2, 4);

            GenLayer addBeach = new GenLayerAddBeach(50, zoomed2);
            GenLayer zoomed3 = new GenLayerZoom(seed, addBeach);
            GenLayer addRiver = new GenLayerAddRiver(1000, zoomed3);

            GenLayer result = GenLayerZoom.Magnify(seed, addRiver, 2);

            // GenLayer biomesAdded = new GenLayerBiome(seed, zoomed0);
            // GenLayer zoomed2 = new GenLayerZoom(seed, zoomed1);
            return result;
        }

        public static int SelectRandom(int seed, params int[] arrys)
        {
            return arrys[new Random(seed).Next(maxValue: arrys.Length)];
        }

        public static int SelectModeOrRandom(int seed, int a, int b, int c, int d)
        {
            if (a == b || a == c || a == d)
            {
                return a;
            }
            else if (b == c || b == d)
            {
                return b;
            }
            else if (c == d)
            {
                return c;
            }
            else
            {
                return SelectRandom(seed, a, b, c, d);
            }
        }

        public static int GetChunkSeed(int x, int z)
        {
            return z * 16384 + x;
        }
    }
}