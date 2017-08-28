using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Server.World.Biomes;
using MineCase.Server.World.Layer;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace MineCase.Server.World.Generation
{
    [StatelessWorker]
    internal class BiomeGeneratorFlatGrain : Grain, IBiomeGenerator
    {
        private GenLayer _genBiomes;
        private GenLayer _biomeIndexLayer;

        private int _seed;

        /*
        public override Task OnActivateAsync()
        {
            _seed = (int)this.GetPrimaryKeyLong();
            GenLayer[] agenlayer = GenLayer.initializeAllBiomeGenerators(_seed, worldTypeIn, settings);
            agenlayer = getModdedBiomeGenerators(worldTypeIn, seed, agenlayer);
            _genBiomes = agenlayer[0];
            _biomeIndexLayer = agenlayer[1];
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns an array of biomes for the location input.
        /// </summary>
        public void GetBiomesForGeneration(Biome[] biomes, int x, int z, int width, int height)
        {
            if (biomes == null || biomes.Length < width * height)
            {
                biomes = new Biome[width * height];
            }

            int[] aint = _genBiomes.getInts(x, z, width, height);

            for (int i = 0; i < width * height; ++i)
            {
                biomes[i] = Biome.GetBiome(aint[i]);
            }
        }
        */
    }
}