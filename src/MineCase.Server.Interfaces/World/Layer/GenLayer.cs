using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Generation;

namespace MineCase.Server.World.Layer
{
    public abstract class GenLayer
    {
        /** base seed to the LCG prng provided via the constructor */
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
            GenLayer addIsland0 = new GenLayerIsland(1, null);
            GenLayer zoomed0 = new GenLayerZoom(seed, addIsland0);
            GenLayer addIsland1 = new GenLayerAddIsland(2, zoomed0);
            GenLayer zoomed1 = new GenLayerZoom(seed, addIsland1);
            GenLayer addIsland2 = new GenLayerAddIsland(50, zoomed1);
            GenLayer addIsland3 = new GenLayerAddIsland(70, addIsland2);
            GenLayer zoomed2 = new GenLayerZoom(seed, addIsland3);
            GenLayer biomesAdded = new GenLayerBiome(seed, zoomed2);
            GenLayer result = GenLayerZoom.Magnify(seed, biomesAdded, 4);

            // GenLayer biomesAdded = new GenLayerBiome(seed, zoomed0);
            // GenLayer zoomed2 = new GenLayerZoom(seed, zoomed1);
            return result;
        }
    }
}