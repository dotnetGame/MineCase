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
    internal class ChunkGeneratorFlatGrain : Grain, IChunkGeneratorFlat
    {
        public Task<ChunkColumn> Generate(int x, int z, GeneratorSettings settings)
        {
            ChunkColumn chunkColumn=new ChunkColumn();
            GenerateChunk(chunkColumn,x,z,settings);
            PopulateChunk(chunkColumn,x,z,settings);
            return Task.FromResult(chunkColumn);
        }

        public Task GenerateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            //按照flat模式每层的设置给chunk赋值
            for (int i = 0; i < settings.flatGeneratorInfo.FlatBlockId.Length; ++i)
            {
                BlockState state = settings.flatGeneratorInfo.FlatBlockId[i];
                if (state != null)
                {
                    for (int j = 0; j < 16; ++j)
                    {
                        for (int k = 0; k < 16; ++k)
                        {
                            chunk.SetBlockState(j, i, k, state);
                        }
                    }
                }
            }
            // todo biomes
            chunk.GenerateSkylightMap();
            return Task.CompletedTask;
        }

        public Task PopulateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            // TODO generator tree, grass, structures\
            return Task.CompletedTask;
        }

    }
}