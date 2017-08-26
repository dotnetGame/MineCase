using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ImageSharp;
using MineCase.Algorithm.Noise;
using Xunit;

namespace MineCase.UnitTest
{
    public class NoiseTest
    {
        public static readonly string RootDir = @"F:\Work\Repository\MineCase\tests\UnitTest\bin\Debug";

        [Fact]
        public void TestPerlinNoise3D()
        {
            const int xExtent = 100;
            const int yExtent = 100;

            using (var file = File.OpenWrite(Path.Combine(RootDir, "PerlinNoise3D.bmp")))
            using (var image = new Image<ImageSharp.PixelFormats.Rgb24>(xExtent, yExtent))
            {
                var noise = new PerlinNoise();
                for (int x = 0; x < xExtent; x++)
                {
                    for (int y = 0; y < yExtent; y++)
                    {
                        var color = (byte)noise.Noise(x, 0, y);
                        image[x, y] = new ImageSharp.PixelFormats.Rgb24(color, color, color);
                    }
                }

                image.SaveAsBmp(file);
            }
        }
    }
}
