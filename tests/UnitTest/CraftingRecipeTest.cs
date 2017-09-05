using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MineCase.UnitTest
{
    public class CraftingRecipeTest
    {
        public readonly string RootDir;

        public CraftingRecipeTest()
        {
            RootDir = SetRootDir();
        }

        private static string SetRootDir([CallerFilePath]string fileName = null) =>
            Path.Combine(Path.GetDirectoryName(fileName), @"../../data");

        [Fact]
        public async Task TestCraftingRecipeLoader()
        {
            var loader = new CraftingRecipeLoader();
            using (var sr = new StreamReader(File.OpenRead(Path.Combine(RootDir, "crafting.txt"))))
            {
                await loader.LoadRecipes(sr);
            }

            var recipes = loader.Recipes;
            Assert.Equal(13, recipes.Count);
        }
    }
}
