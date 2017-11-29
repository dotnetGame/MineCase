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
    internal class DropBlockMappingGrain : Grain, IDropBlockMapping
    {
        private const string _mappingFileName = "dropblock.txt";

        private DropBlockMappingMatcher _mappingMatcher;
        private IFileProvider _fileProvider;

        public DropBlockMappingGrain()
        {
            _fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory);
        }

        public Task<uint> DropBlock(uint itemId, BlockState block)
        {
            return Task.FromResult(_mappingMatcher.DropBlock(itemId, block));
        }

        public override async Task OnActivateAsync()
        {
            var file = _fileProvider.GetFileInfo(_mappingFileName);

            var recipeLoader = new DropBlockMappingLoader();
            using (var sr = new StreamReader(file.CreateReadStream()))
                await recipeLoader.LoadMapping(sr);
            _mappingMatcher = new DropBlockMappingMatcher(recipeLoader.Mapping);
        }
    }
}
