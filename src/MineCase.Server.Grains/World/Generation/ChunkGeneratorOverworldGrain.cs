using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace MineCase.Server.World.Generation
{
    [StatelessWorker]
    internal class ChunkGeneratorOverWorldGrain : IChunkGeneratorOverworld
    {
        public Task<ChunkColumn> Generate(int x, int z)
        {
            ChunkColumn chunkColumn=new ChunkColumn();

            return Task.FromResult(chunkColumn);
        }

        public Task GenerateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {

            return Task.CompletedTask;
        }

        public Task PopulateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            return Task.CompletedTask;
        }

        private Task<double[]> GenerateDensityMap(int x,int y,int z, GeneratorSettings settings)
        {
            double [] densityMap=new double[5*5*33];

            return Task.FromResult(densityMap);
        }
    }
}