using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using ImageSharp;
using MineCase.Algorithm;
using Xunit;

namespace MineCase.UnitTest
{
    public class RNGTest
    {
        public readonly string RootDir;

        public RNGTest()
        {
            RootDir = SetRootDir();
        }

        private static string SetRootDir([CallerFilePath]string fileName = null) =>
            Path.Combine(Path.GetDirectoryName(fileName), "bin");

        [Fact]
        public void TestNext()
        {
            UniformRNG rng = new UniformRNG(0x123456789);
            int first = rng.NextInt(), second = rng.NextInt();
            for (int i = 0; i < 1000000; ++i)
            {
                first = second;
                second = rng.NextInt();
                Assert.NotEqual(first, second);
            }
        }

        [Fact]
        public void TestUniform()
        {
            UniformRNG rng = new UniformRNG(0x123456789);
            var i1 = rng.Uniform(1, 2);
            Assert.Equal(1, i1);
            uint a = 1, b = 2;
            var i2 = rng.Uniform(a, b);
            Assert.Equal((uint)1, i2);
        }

        [Fact]
        public void TestRNGImg()
        {
            UniformRNG rng = new UniformRNG(0);
            int[] bucket = new int[1000];

            const int xExtent = 1024;
            const int yExtent = 400;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "rng.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                for (int i = 0; i < 100000; ++i)
                {
                    bucket[rng.Uniform(0, 1000)]++;
                }

                for (int i = 0; i < 1000; ++i)
                {
                    for (int j = 0; j < bucket[i]; ++j)
                    {
                        image[i, j] = new ImageSharp.PixelFormats.Rgb24((byte)0xFF, (byte)0x69, (byte)0xB4);
                    }
                }

                image.SaveAsBmp(file);
            }
        }
    }
}
