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
            UniformRNG rng = new UniformRNG(0);
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
            UniformRNG rng = new UniformRNG(0);

            var i1 = rng.Uniform(1, 2);
            Assert.Equal(1, i1);
            var i2 = rng.Uniform(1U, 2U);
            Assert.Equal(1U, i2);
            var i3 = rng.Uniform(1.0f, 1.0f);
            Assert.Equal(1.0f, i3);
            var i4 = rng.Uniform(1.0, 1.0);
            Assert.Equal(1.0, i4);
        }

        [Fact]
        public void TestFrequencyImg()
        {
            UniformRNG rng = new UniformRNG(0);
            int[] bucket = new int[100];

            const int xExtent = 100;
            const int yExtent = 200;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "rng_frequency.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                for (int i = 0; i < 10000; ++i)
                {
                    bucket[rng.Uniform(0, xExtent)]++;
                }

                for (int i = 0; i < xExtent; ++i)
                {
                    for (int j = 0; j < bucket[i]; ++j)
                    {
                        image[i, yExtent - j] = new ImageSharp.PixelFormats.Rgb24((byte)0xFF, (byte)0x69, (byte)0xB4);
                    }
                }

                image.SaveAsBmp(file);
            }
        }

        [Fact]
        public void TesIntNoiseImg()
        {
            UniformRNG rng = new UniformRNG(0);

            const int xExtent = 100;
            const int yExtent = 100;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "rng_int_noise.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                for (int i = 0; i < xExtent; ++i)
                {
                    for (int j = 0; j < yExtent; ++j)
                    {
                        var color = (byte)rng.Uniform(0, 255);
                        image[i, j] = new ImageSharp.PixelFormats.Rgb24(color, color, color);
                    }
                }

                image.SaveAsBmp(file);
            }
        }

        [Fact]
        public void TesFloatNoiseImg()
        {
            UniformRNG rng = new UniformRNG(0);

            const int xExtent = 100;
            const int yExtent = 100;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "rng_float_noise.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                for (int i = 0; i < xExtent; ++i)
                {
                    for (int j = 0; j < yExtent; ++j)
                    {
                        var color = (byte)(rng.Uniform(0.0f, 2.55f) * 100);
                        image[i, j] = new ImageSharp.PixelFormats.Rgb24(color, color, color);
                    }
                }

                image.SaveAsBmp(file);
            }
        }

        [Fact]
        public void TesDoubleNoiseImg()
        {
            UniformRNG rng = new UniformRNG(0);

            const int xExtent = 100;
            const int yExtent = 100;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "rng_double_noise.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                for (int i = 0; i < xExtent; ++i)
                {
                    for (int j = 0; j < yExtent; ++j)
                    {
                        var color = (byte)(rng.Uniform(0.0, 2.55) * 100);
                        image[i, j] = new ImageSharp.PixelFormats.Rgb24(color, color, color);
                    }
                }

                image.SaveAsBmp(file);
            }
        }
    }
}
