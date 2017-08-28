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
            ChunkColumn chunkColumn = new ChunkColumn();
            chunkColumn.Sections = new ChunkSection[16];
            for (int i = 0; i < chunkColumn.Sections.Length; ++i)
            {
                chunkColumn.Sections[i] = new ChunkSection
                {
                    BitsPerBlock = 13,
                    Blocks = new Block[4096]
                };
                for (int j = 0; j < chunkColumn.Sections[i].Blocks.Length; ++j)
                {
                    chunkColumn.Sections[i].Blocks[j] = new Block();
                }
            }

            chunkColumn.SectionBitMask = 0b1111_1111_1111_1111;
            chunkColumn.Biomes = new byte[256];

            GenerateChunk(chunkColumn, x, z, settings);
            PopulateChunk(chunkColumn, x, z, settings);
            return Task.FromResult(chunkColumn);
        }

        public Task GenerateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            // 按照flat模式每层的设置给chunk赋值
            for (int i = 0; i < settings.FlatGeneratorInfo.FlatBlockId.Length; ++i)
            {
                BlockState state = settings.FlatGeneratorInfo.FlatBlockId[i];
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