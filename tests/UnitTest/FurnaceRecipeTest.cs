using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Block;
using MineCase.Item;
using Xunit;

namespace MineCase.UnitTest
{
    public class FurnaceRecipeTest
    {
        public readonly string RootDir;

        public FurnaceRecipeTest()
        {
            RootDir = SetRootDir();
        }

        private static string SetRootDir([CallerFilePath]string fileName = null) =>
            Path.Combine(Path.GetDirectoryName(fileName), @"../../data");

        [Fact]
        public Task TestFurnaceRecipeLoader()
        {
            // Wait for api update
            /*
            var loader = new FurnaceRecipeLoader();
            using (var sr = new StreamReader(File.OpenRead(Path.Combine(RootDir, "furnace.txt"))))
            {
                await loader.LoadRecipes(sr);
            }

            var recipes = loader.Recipes;
            Assert.Equal(2, recipes.Count);
            var fuels = loader.Fuels;
            Assert.Equal(4, fuels.Count);
            */
            return Task.CompletedTask;
        }

        [Fact]
        public Task TestFurnaceRecipeMatcher()
        {
            // Wait for api update
            /*
            var loader = new FurnaceRecipeLoader();
            using (var sr = new StreamReader(File.OpenRead(Path.Combine(RootDir, "furnace.txt"))))
            {
                await loader.LoadRecipes(sr);
            }

            var matcher = new FurnaceRecipeMatcher(loader.Recipes, loader.Fuels);
            var recipe = matcher.FindRecipe(
                new Slot { BlockId = (short)BlockStates.Wood().Id, ItemCount = 1 });
            var fuel = matcher.FindFuel(
                new Slot { BlockId = (short)BlockStates.Wood().Id, ItemCount = 1 });

            Assert.NotNull(recipe);
            Assert.Equal(ItemStates.Coal(CoalType.Charcoal), new ItemState { Id = (uint)recipe.Output.BlockId, MetaValue = (uint)recipe.Output.ItemDamage });
            Assert.Equal((short)BlockStates.Wood().Id, fuel.Slot.BlockId);
            */
            return Task.CompletedTask;
        }
    }
}
