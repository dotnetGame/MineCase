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

            return Task.FromResult(new FindCraftingRecipeResult
            {
                Result = _recipes[0].Result,
                AfterTake = craftingGrid
            });
        }
    }
}
