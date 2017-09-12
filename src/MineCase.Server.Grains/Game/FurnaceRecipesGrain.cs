using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using MineCase.Algorithm;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [StatelessWorker]
    internal class FurnaceRecipesGrain : Grain, IFurnaceRecipes
    {
        private const string _recipesFileName = "furnace.txt";

        private FurnaceRecipeMatcher _recipeMatcher;
        private IFileProvider _fileProvider;

        public FurnaceRecipesGrain()
        {
            _fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory);
        }

        public override async Task OnActivateAsync()
        {
            var file = _fileProvider.GetFileInfo(_recipesFileName);

            var recipeLoader = new FurnaceRecipeLoader();
            using (var sr = new StreamReader(file.CreateReadStream()))
                await recipeLoader.LoadRecipes(sr);
            _recipeMatcher = new FurnaceRecipeMatcher(recipeLoader.Recipes, recipeLoader.Fuels);
        }

        public Task<FindFurnaceRecipeResult> FindRecipe(Slot input, Slot fuel)
        {
            return Task.FromResult(_recipeMatcher.FindRecipe(input, fuel));
        }
    }
}
