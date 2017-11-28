using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
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
            Assert.Equal(13 + 26, recipes.Count);
        }

        [Fact]
        public async Task TestCraftingRecipeMatcher()
        {
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
        }

        [Fact]
        public async Task TestTools()
        {
            var loader = new CraftingRecipeLoader();
            using (var sr = new StreamReader(File.OpenRead(Path.Combine(RootDir, "crafting.txt"))))
            {
                await loader.LoadRecipes(sr);
            }

            var matcher = new CraftingRecipeMatcher(loader.Recipes);
            var recipe = matcher.FindRecipe(new Slot[,]
            {
                { new Slot { BlockId = (short)BlockStates.WoodPlanks().Id, ItemCount = 1 }, Slot.Empty, Slot.Empty },
                { new Slot { BlockId = (short)BlockStates.WoodPlanks().Id, ItemCount = 1 }, new Slot { BlockId = (short)ItemId.Stick, ItemCount = 1 }, new Slot { BlockId = (short)ItemId.Stick, ItemCount = 1 }},
                { new Slot { BlockId = (short)BlockStates.WoodPlanks().Id, ItemCount = 1 }, Slot.Empty, Slot.Empty }
            });
            Assert.NotNull(recipe);
            Assert.Equal((short)ItemId.WoodenPickaxe, recipe.Result.BlockId);
            Assert.True(recipe.AfterTake.Cast<Slot>().All(o => o.IsEmpty));
        }
    }
}
