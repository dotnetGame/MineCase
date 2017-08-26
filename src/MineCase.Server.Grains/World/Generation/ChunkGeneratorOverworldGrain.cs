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
        public ChunkGeneratorOverWorldGrain()
        {

        }

        public Task<ChunkColumn> generateChunk(int x, int z, long seed)
        {
            ChunkColumn chunkColumn=new ChunkColumn();

            return Task.FromResult(chunkColumn);
        }

        public Task populateChunk(ChunkColumn chunk, int x, int z, long seed)
        {
            return Task.CompletedTask;
        }

        private Task<double[]> generateDensityMap(int x,int y,int z)
        {
            double [] densityMap=new double[5*5*33];

            return Task.FromResult(densityMap);
        }
    }
}