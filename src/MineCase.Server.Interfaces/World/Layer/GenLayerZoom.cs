using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Layer
{
    public class GenLayerZoom : GenLayer
    {
        public GenLayerZoom(int seed, GenLayer parent)
            : base(seed, parent)
        {
        }

        public override int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            // parent尺度是本层的1/2
            int parentAreaX = areaX / 2;
            int parentAreaY = areaY / 2;

            // +2添加边界
            int parentWidth = areaWidth / 2 + 2;
            int parentHeight = areaHeight / 2 + 2;

            int[,] result = new int[areaHeight, areaWidth];
            int[,] parentResult = _parent.GetInts(parentAreaX, parentAreaY, parentWidth, parentHeight);
            for (int y = 0; y < areaHeight - 1; ++y)
            {
                for (int x = 0; x < areaWidth - 1; ++x)
                {
                    // Random random = new Random((parentAreaY + y / 2) * 16384 + parentAreaX + x / 2);
                    result[y, x] = parentResult[y / 2, x / 2];
                }
            }

            return result;
        }

        public static GenLayer Magnify(int seed, GenLayer layer, int times)
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
