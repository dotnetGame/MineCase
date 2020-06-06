using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using MineCase.Loot;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [StatelessWorker]
    internal class LootTableGrain : Grain, ILootTable
    {
        private const string _blockLootTablePath = "./data/minecraft/loot_tables/blocks/";
        private const string _chestsLootTablePath = "./data/minecraft/loot_tables/chests/";
        private const string _entitiesLootTablePath = "./data/minecraft/loot_tables/entities/";
        private const string _gameplayLootTablePath = "./data/minecraft/loot_tables/gameplay/";

        private IFileProvider _fileProvider;

        public LootTableGrain()
        {
            _fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory);
        }

        public override async Task OnActivateAsync()
        {
            await LoadLootTable(_blockLootTablePath);
            await LoadLootTable(_chestsLootTablePath);
            await LoadLootTable(_entitiesLootTablePath);
            await LoadLootTable(_gameplayLootTablePath);

            // _recipeMatcher = new FurnaceRecipeMatcher(recipeLoader.Recipes, recipeLoader.Fuels);
        }

        private async Task LoadLootTable(string path)
        {
            IDirectoryContents files = _fileProvider.GetDirectoryContents(path);
            foreach (IFileInfo eachFile in files)
            {
                var lootTableLoader = new LootTableLoader();
                using (var sr = new StreamReader(eachFile.CreateReadStream()))
                    await lootTableLoader.LoadLootTable(sr);
            }
        }
    }
}
