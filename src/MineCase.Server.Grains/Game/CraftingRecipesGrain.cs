using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [StatelessWorker]
    internal class CraftingRecipesGrain : Grain, ICraftingRecipes
    {
        private const string _recipesFileName = "crafting.txt";

        private List<CraftingRecipe> _recipes;
        private IFileProvider _fileProvider;

        public CraftingRecipesGrain()
        {
            _fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory);
        }

        public override async Task OnActivateAsync()
        {
            var file = _fileProvider.GetFileInfo(_recipesFileName);

            var recipeLoader = new CraftingRecipeLoader();
            using (var sr = new StreamReader(file.CreateReadStream()))
                await recipeLoader.LoadRecipes(sr);
            _recipes = recipeLoader.Recipes;
        }

        public Task<FindCraftingRecipeResult> FindRecipe(Slot[,] craftingGrid)
        {
            int gridLeft = 2, gridTop = 2;
            int gridRight = 0, gridBottom = 0;

            for (int x = 0; x < craftingGrid.GetUpperBound(0) + 1; x++)
            {
                for (int y = 0; y < craftingGrid.GetUpperBound(1) + 1; y++)
                {
                    if (!craftingGrid[x, y].IsEmpty)
                    {
                        gridRight = Math.Max(x, gridRight);
                        gridBottom = Math.Max(y, gridBottom);
                        gridLeft = Math.Min(x, gridLeft);
                        gridTop = Math.Min(y, gridTop);
                    }
                }
            }

            var gridWidth = gridRight - gridLeft + 1;
            var gridHeight = gridBottom - gridTop + 1;
            var newGrid = CropCraftingGrid(craftingGrid, gridLeft, gridTop, gridWidth, gridHeight);
            var result = FindRecipeCropped(newGrid);
            if (result != null)
            {
                result.AfterTake = RestoreCroppedCraftingGrid(newGrid, gridLeft, gridTop, craftingGrid.GetUpperBound(0) + 1, craftingGrid.GetUpperBound(1));
                return Task.FromResult(result);
            }

            return Task.FromResult<FindCraftingRecipeResult>(null);
        }

        private FindCraftingRecipeResult FindRecipeCropped(Slot[,] craftingGrid)
        {
            var gridWidth = craftingGrid.GetUpperBound(0) + 1;
            var gridHeight = craftingGrid.GetUpperBound(1) + 1;

            foreach (var recipe in _recipes)
            {
                var maxOfX = gridWidth - recipe.Width;
                var maxOfY = gridHeight - recipe.Height;

                for (int x = 0; x <= maxOfX; x++)
                {
                    for (int y = 0; y <= maxOfY; y++)
                    {
                        var result = MatchRecipe(craftingGrid, recipe, x, y);
                        if (result != null) return result;
                    }
                }
            }

            return null;
        }

        private FindCraftingRecipeResult MatchRecipe(Slot[,] craftingGrid, CraftingRecipe recipe, int xOffset, int yOffset)
        {
            var gridWidth = craftingGrid.GetUpperBound(0) + 1;
            var gridHeight = craftingGrid.GetUpperBound(1) + 1;

            var hasMatched = new bool[gridWidth, gridHeight];
            var after = (Slot[,])craftingGrid.Clone();
            for (int i = 0; i < recipe.Slots.Length; i++)
            {
                ref var recipeSlot = ref recipe.Slots[i];

                // Anywhere, 稍后处理
                if (recipeSlot.X < 0 || recipeSlot.Y < 0) continue;

                var x = recipeSlot.X + xOffset;
                var y = recipeSlot.Y + yOffset;
                ref var gridSlot = ref craftingGrid[x, y];
                if (recipeSlot.X >= gridWidth || recipeSlot.Y >= gridHeight ||
                    recipeSlot.Slot.BlockId != gridSlot.BlockId ||
                    recipeSlot.Slot.ItemCount > gridSlot.ItemCount ||
                    (recipeSlot.Slot.ItemDamage >= 0 && recipeSlot.Slot.ItemDamage != gridSlot.ItemDamage))
                    return null;
                hasMatched[recipeSlot.X + xOffset, recipeSlot.Y + yOffset] = true;
                after[x, y].ItemCount -= recipeSlot.Slot.ItemCount;
                after[x, y].MakeEmptyIfZero();
            }

            // 处理 Anywhere
            for (int i = 0; i < recipe.Slots.Length; i++)
            {
                ref var recipeSlot = ref recipe.Slots[i];

                // Anywhere, 稍后处理
                if (recipeSlot.X >= 0 && recipeSlot.Y >= 0) continue;
            }

            return new FindCraftingRecipeResult
            {
                Result = _recipes[0].Result,
                AfterTake = craftingGrid
            };
        }

        private static Slot[,] CropCraftingGrid(Slot[,] sourceGrid, int gridLeft, int gridTop, int gridWidth, int gridHeight)
        {
            var slots = new Slot[gridWidth, gridHeight];
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    slots[x, y] = sourceGrid[x + gridLeft, y + gridTop];
                }
            }

            NormalizeSlots(slots);
            return slots;
        }

        private Slot[,] RestoreCroppedCraftingGrid(Slot[,] craftingGrid, int gridLeft, int gridTop, int originWidth, int originHeight)
        {
            var slots = new Slot[originWidth, originHeight];
            for (int y = 0; y < craftingGrid.GetUpperBound(1) + 1; y++)
            {
                for (int x = 0; x < craftingGrid.GetUpperBound(0) + 1; x++)
                {
                    slots[x + gridLeft, y + gridTop] = craftingGrid[x, y];
                }
            }

            NormalizeSlots(slots);
            return slots;
        }

        private static void NormalizeSlots(Slot[,] slots)
        {
            for (int x = 0; x < slots.GetUpperBound(0) + 1; x++)
            {
                for (int y = 0; y < slots.GetUpperBound(1) + 1; y++)
                {
                    slots[x, y].MakeEmptyIfZero();
                }
            }
        }
    }
}
