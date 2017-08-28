using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Layer
{
    public class GenLayerZoom : GenLayer
    {
        public GenLayerZoom(int seed, GenLayer layer)
        :base(seed)
        {
            base._parent = layer;
        }

        /**
        * Returns a list of integer values generated by this layer. These may be interpreted as temperatures, rainfall
        * amounts, or Biome ID's based on the particular GenLayer subclass.
        */
        public override int[] getInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            int [] aint2=new int[areaWidth*areaHeight];
            return aint2;
        }

        /**
        * Magnify a layer. Parms are seed adjustment, layer, number of times to magnify
        */
        public static GenLayer magnify(int seed, GenLayer layer, int times)
        {
            GenLayer genlayer = layer;

            for (int i = 0; i < times; ++i)
            {
                genlayer = new GenLayerZoom(seed + i, genlayer);
            }

            return genlayer;
        }
    }

}
