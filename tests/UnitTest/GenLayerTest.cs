using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Server.World.Layer;
using Xunit;

namespace MineCase.UnitTest
{
    public class GenLayerTest
    {
        [Fact]
        public void Test1()
        {
            using (StreamWriter sw = new StreamWriter("E:/GenLayerTest.txt"))
            {
                GenLayer layer = GenLayer.InitAllLayer(1);
                int x = 8;
                int z = 8;
                int[,] biomeIds = layer.GetInts(16 * x - 8, 16 * z - 8, 32, 32);
                sw.WriteLine("pre: ");
                for (int i = 0; i < 32; ++i)
                {
                    for (int j = 0; j < 32; ++j)
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
