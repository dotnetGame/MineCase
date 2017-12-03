using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.World;
using MineCase.World.Generation;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace MineCase.Server.World.Generation
{
    [StatelessWorker]
    internal class ChunkGeneratorFlatGrain : Grain, IChunkGeneratorFlat
    {
        public Task<ChunkColumnCompactStorage> Generate(IWorld world, int x, int z, GeneratorSettings settings)
        {
            var chunkColumn = new ChunkColumnCompactStorage();
            for (int i = 0; i < chunkColumn.Sections.Length; ++i)
                chunkColumn.Sections[i] = new ChunkSectionCompactStorage(true);

            GenerateChunk(world, chunkColumn, x, z, settings);
            PopulateChunk(world, chunkColumn, x, z, settings);
            return Task.FromResult(chunkColumn);
        }

        private void GenerateChunk(IWorld world, ChunkColumnCompactStorage chunk, int x, int z, GeneratorSettings settings)
        {
            // 按照flat模式每层的设置给chunk赋值
            for (int y = 0; y < settings.FlatBlockId.Length; ++y)
            {
                var section = chunk.Sections[y / 16];
                var state = settings.FlatBlockId[y];
                if (state != null)
                {
                    for (int j = 0; j < 16; ++j)
                    {
                        for (int k = 0; k < 16; ++k)
                            section.Data[j, y % 16, k] = state.Value;
                    }
                }

                for (int i = 0; i < section.SkyLight.Storage.Length; i++)
                    section.SkyLight.Storage[i] = 0xFF;
            }

            // todo biomes
        }

        private void PopulateChunk(IWorld world, ChunkColumnCompactStorage chunk, int x, int z, GeneratorSettings settings)
        {
            // TODO generator tree, grass, structures\
        }
    }
}