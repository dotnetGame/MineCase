using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MineCase.Algorithm;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [StatelessWorker]
    internal class CraftingRecipesGrain : Grain, ICraftingRecipes
    {
        private const string _recipesFileName = "crafting.txt";

        private CraftingRecipeMatcher _recipeMatcher;
        private IFileProvider _fileProvider;

        public CraftingRecipesGrain()
        {
            _fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory);
        }

        public Task<FindCraftingRecipeResult> FindRecipe(Immutable<Slot[,]> craftingGrid)
        {
            return Task.FromResult(_recipeMatcher.FindRecipe(craftingGrid.Value));
        }

        public override async Task OnActivateAsync()
        {
            var file = _fileProvider.GetFileInfo(_recipesFileName);

            var recipeLoader = new CraftingRecipeLoader();
            using (var sr = new StreamReader(file.CreateReadStream()))
                await recipeLoader.LoadRecipes(sr);
            _recipeMatcher = new CraftingRecipeMatcher(recipeLoader.Recipes);
        }
    }
}
