using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Block;
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
        public Task TestCraftingRecipeLoader()
        {
            // Wait for api update
            /*
            var loader = new CraftingRecipeLoader();
            using (var sr = new StreamReader(File.OpenRead(Path.Combine(RootDir, "crafting_test.txt"))))
            {
                await loader.LoadRecipes(sr);
            }

            var recipes = loader.Recipes;

            // count test
            Assert.Equal(13, recipes.Count);
            */
            return Task.CompletedTask;
        }

        [Fact]
        public Task TestCraftingRecipeMatcher()
        {
            // Wait for api update
            /*
            var loader = new CraftingRecipeLoader();
            using (var sr = new StreamReader(File.OpenRead(Path.Combine(RootDir, "crafting.txt"))))
            {
                await loader.LoadRecipes(sr);
            }

            var matcher = new CraftingRecipeMatcher(loader.Recipes);
            var recipe = matcher.FindRecipe(new Slot[,]
            {
                { Slot.Empty, Slot.Empty, Slot.Empty },
                { new Slot { BlockId = (short)BlockStates.Wood().Id, ItemCount = 1 }, Slot.Empty, Slot.Empty },
                { Slot.Empty, Slot.Empty, Slot.Empty },
            });
            Assert.NotNull(recipe);
            Assert.Equal((short)BlockStates.WoodPlanks().Id, recipe.Result.BlockId);
            Assert.True(recipe.AfterTake.Cast<Slot>().All(o => o.IsEmpty));
            */
            return Task.CompletedTask;
        }
    }
}
