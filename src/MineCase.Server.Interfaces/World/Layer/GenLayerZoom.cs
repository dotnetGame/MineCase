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
            int parentAreaX = areaX > 0 ? areaX / 2 : (areaX - 1) / 2;
            int parentAreaY = areaY > 0 ? areaY / 2 : (areaY - 1) / 2;

            // +2添加边界
            int parentWidth = areaWidth / 2 + 2;
            int parentHeight = areaHeight / 2 + 2;

            // parentRes是本层的1/4
            int[,] parentRes = _parent.GetInts(parentAreaX, parentAreaY, parentWidth, parentHeight);
            int tmpWidth = (parentWidth - 1) * 2;
            int tmpHeight = (parentHeight - 1) * 2;

            // 临时结果
            int[,] tmp = new int[tmpHeight, tmpWidth];

            for (int parentY = 0; parentY < parentHeight - 1; ++parentY)
            {
                // parent当前点的值
                int parentValue = parentRes[parentY, 0];

                // parent当前点y+1点的值
                int parentValueY1 = parentRes[parentY + 1, 0];

                for (int parentX = 0; parentX < parentWidth - 1; ++parentX)
                {
                    int randomSeed = GetChunkSeed((parentX + parentAreaX) * 2, (parentY + parentAreaY) * 2);

                    // parent当前点x+1点的值
                    int parentValueX1 = parentRes[parentY, parentX + 1];

                    // parent当前点x+1, y+1点的值
                    int parentValueX1Y1 = parentRes[parentY + 1, parentX + 1];

                    // 当前点值 = parent点值
                    tmp[parentY * 2, parentX * 2] = parentValue;

                    // 当前点y+1值 = 在 parent点、parent点y+1 中随机选
                    tmp[parentY * 2 + 1, parentX * 2] = SelectRandom(randomSeed, parentValue, parentValueY1);

                    // 当前点值 = 在 parent点、parent点x+1 中随机选
                    tmp[parentY * 2, parentX * 2 + 1] = SelectRandom(randomSeed, parentValue, parentValueX1);

                    // 当前点y+1值 = parent四个点值中的众数或随机选
                    tmp[parentY * 2 + 1, parentX * 2 + 1] = SelectModeOrRandom(randomSeed, parentValue, parentValueX1, parentValueY1, parentValueX1Y1);

                    // parent当前点移动x+1
                    parentValue = parentValueX1;
                    parentValueY1 = parentValueX1Y1;
                }
            }

            int[,] result = new int[areaHeight, areaWidth];

            // tmp和result尺寸可能不同，这里把tmp中间部分复制到result
            int areaOffsetX = Math.Abs(areaX % 2);
            int areaOffsetY = Math.Abs(areaY % 2);
            for (int resultY = 0; resultY < areaHeight; ++resultY)
            {
                // System.Array.Copy(tmp, (resultY + areaY % 2) * tmpWidth + areaX % 2, result, resultY * areaWidth, areaWidth);
                for (int resultX = 0; resultX < areaWidth; ++resultX)
                {
                    result[resultY, resultX] = tmp[resultY + areaOffsetY, resultX + areaOffsetX];
                }
            }

            return result;
        }

        public static GenLayer Magnify(int seed, GenLayer layer, int times)
        {
            GenLayer genlayer = layer;

            for (int i = 0; i < times; ++i)
            {
                genlayer = new GenLayerZoom(seed + 1, genlayer);
            }

            return genlayer;
        }
    }
}
