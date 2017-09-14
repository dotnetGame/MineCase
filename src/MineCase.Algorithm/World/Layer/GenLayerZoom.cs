using System;

namespace MineCase.Algorithm.World.Layer
{
    public class GenLayerZoom : GenLayer
    {
        public GenLayerZoom(int seed, GenLayer parent)
            : base(seed, parent)
        {
        }

        public override int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            // parent�߶��Ǳ����1/2
            int parentAreaX = areaX > 0 ? areaX / 2 : (areaX - 1) / 2;
            int parentAreaY = areaY > 0 ? areaY / 2 : (areaY - 1) / 2;

            // +2��ӱ߽�
            int parentWidth = areaWidth / 2 + 2;
            int parentHeight = areaHeight / 2 + 2;

            // parentRes�Ǳ����1/4
            int[,] parentRes = _parent.GetInts(parentAreaX, parentAreaY, parentWidth, parentHeight);
            int tmpWidth = (parentWidth - 1) * 2;
            int tmpHeight = (parentHeight - 1) * 2;

            // ��ʱ���
            int[,] tmp = new int[tmpHeight, tmpWidth];

            for (int parentY = 0; parentY < parentHeight - 1; ++parentY)
            {
                // parent��ǰ���ֵ
                int parentValue = parentRes[parentY, 0];

                // parent��ǰ��y+1���ֵ
                int parentValueY1 = parentRes[parentY + 1, 0];

                for (int parentX = 0; parentX < parentWidth - 1; ++parentX)
                {
                    int randomSeed = GetChunkSeed((parentX + parentAreaX) * 2, (parentY + parentAreaY) * 2);

                    // parent��ǰ��x+1���ֵ
                    int parentValueX1 = parentRes[parentY, parentX + 1];

                    // parent��ǰ��x+1, y+1���ֵ
                    int parentValueX1Y1 = parentRes[parentY + 1, parentX + 1];

                    // ��ǰ��ֵ = parent��ֵ
                    tmp[parentY * 2, parentX * 2] = parentValue;

                    // ��ǰ��y+1ֵ = �� parent�㡢parent��y+1 �����ѡ
                    tmp[parentY * 2 + 1, parentX * 2] = SelectRandom(randomSeed, parentValue, parentValueY1);

                    // ��ǰ��ֵ = �� parent�㡢parent��x+1 �����ѡ
                    tmp[parentY * 2, parentX * 2 + 1] = SelectRandom(randomSeed, parentValue, parentValueX1);

                    // ��ǰ��y+1ֵ = parent�ĸ���ֵ�е����������ѡ
                    tmp[parentY * 2 + 1, parentX * 2 + 1] = SelectModeOrRandom(randomSeed, parentValue, parentValueX1, parentValueY1, parentValueX1Y1);

                    // parent��ǰ���ƶ�x+1
                    parentValue = parentValueX1;
                    parentValueY1 = parentValueX1Y1;
                }
            }

            int[,] result = new int[areaHeight, areaWidth];

            // tmp��result�ߴ���ܲ�ͬ�������tmp�м䲿�ָ��Ƶ�result
            int areaOffsetX = Math.Abs(areaX % 2);
            int areaOffsetY = Math.Abs(areaY % 2);
            for (int resultY = 0; resultY < areaHeight; ++resultY)
            {
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
