using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Algorithm.World.Layer;
using Xunit;

namespace MineCase.UnitTest
{
    public class GenLayerTest
    {
        [Fact]
        public void Test1()
        {
            using (StreamWriter sw = new StreamWriter("../../GenLayer.txt"))
            {
                GenLayer layer = GenLayer.InitAllLayer(1);
                int x = -4;
                int z = -4;
                int[,] biomeIds = layer.GetInts(16 * x - 16, 16 * z - 16, 48, 48);
                sw.WriteLine("pre: ");
                for (int i = 0; i < 48; ++i)
                {
                    for (int j = 0; j < 48; ++j)
                    {
                        sw.Write(biomeIds[i, j] + " ");
                    }

                    sw.WriteLine();
                }

                biomeIds = layer.GetInts(16 * x, 16 * z, 16, 16);
                sw.WriteLine("suc: ");
                for (int i = 0; i < 16; ++i)
                {
                    for (int j = 0; j < 16; ++j)
                    {
                        sw.Write(biomeIds[i, j] + " ");
                    }

                    sw.WriteLine();
                }
            }
        }
    }
}
