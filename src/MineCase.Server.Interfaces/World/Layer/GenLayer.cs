using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Generation;

namespace MineCase.Server.World.Layer
{
    public abstract class GenLayer
    {
        /** seed from World#getWorldSeed that is used in the LCG prng */
        private int _worldGenSeed;
        /** parent GenLayer that was provided via the constructor */
        protected GenLayer _parent;
        /**
        * final part of the LCG prng that uses the chunk X, Z coords along with the other two seeds to generate
        * pseudorandom numbers
        */
        private int _chunkSeed;
        /** base seed to the LCG prng provided via the constructor */
        protected int _baseSeed;

        public GenLayer(int seed)
        {
            _baseSeed = seed;
        }

        public abstract int[] GetInts(int areaX, int areaY, int areaWidth, int areaHeight);

        /**
         * Initialize layer's local worldGenSeed based on its own baseSeed and the world's global seed (passed in as an
         * argument).
         */
        public void InitWorldGenSeed(int seed)
        {
            _worldGenSeed = seed;
            _worldGenSeed *= _worldGenSeed * 46793005 + 88963407;
            _worldGenSeed += _baseSeed;
            _worldGenSeed *= _worldGenSeed * 46793005 + 88963407;
            _worldGenSeed += _baseSeed;
            _worldGenSeed *= _worldGenSeed * 46793005 + 88963407;
            _worldGenSeed += _baseSeed;
        }

        public void InitChunkSeed(int x, int y)
        {
            _chunkSeed = _worldGenSeed;
            _chunkSeed *= _chunkSeed * 46793005 + 88963407;
            _chunkSeed += x;
            _chunkSeed *= _chunkSeed * 46793005 + 88963407;
            _chunkSeed += y;
            _chunkSeed *= _chunkSeed * 46793005 + 88963407;
            _chunkSeed += x;
            _chunkSeed *= _chunkSeed * 46793005 + 88963407;
            _chunkSeed += y;
        }

        protected int NextInt(int num)
        {
            int i = (_chunkSeed >> 12) % num;

            if (i < 0)
            {
                i += num;
            }

            _chunkSeed *= _chunkSeed * 46793005 + 88963407;
            _chunkSeed += _worldGenSeed;
            return i;
        }

        public static GenLayer[] InitializeAllBiomeGenerators(long seed, WorldType worldType, GeneratorSettings settings)
        {
            /*
            GenLayer genlayer = new GenLayerIsland(1);
            genlayer = new GenLayerFuzzyZoom(2000L, genlayer);
            GenLayer genlayeraddisland = new GenLayerAddIsland(1L, genlayer);
            GenLayer genlayerzoom = new GenLayerZoom(2001L, genlayeraddisland);
            GenLayer genlayeraddisland1 = new GenLayerAddIsland(2L, genlayerzoom);
            genlayeraddisland1 = new GenLayerAddIsland(50L, genlayeraddisland1);
            genlayeraddisland1 = new GenLayerAddIsland(70L, genlayeraddisland1);
            GenLayer genlayerremovetoomuchocean = new GenLayerRemoveTooMuchOcean(2L, genlayeraddisland1);
            GenLayer genlayeraddsnow = new GenLayerAddSnow(2L, genlayerremovetoomuchocean);
            GenLayer genlayeraddisland2 = new GenLayerAddIsland(3L, genlayeraddsnow);
            GenLayer genlayeredge = new GenLayerEdge(2L, genlayeraddisland2, GenLayerEdge.Mode.COOL_WARM);
            genlayeredge = new GenLayerEdge(2L, genlayeredge, GenLayerEdge.Mode.HEAT_ICE);
            genlayeredge = new GenLayerEdge(3L, genlayeredge, GenLayerEdge.Mode.SPECIAL);
            GenLayer genlayerzoom1 = new GenLayerZoom(2002L, genlayeredge);
            genlayerzoom1 = new GenLayerZoom(2003L, genlayerzoom1);
            GenLayer genlayeraddisland3 = new GenLayerAddIsland(4L, genlayerzoom1);
            GenLayer genlayeraddmushroomisland = new GenLayerAddMushroomIsland(5L, genlayeraddisland3);
            GenLayer genlayerdeepocean = new GenLayerDeepOcean(4L, genlayeraddmushroomisland);
            GenLayer genlayer4 = GenLayerZoom.magnify(1000L, genlayerdeepocean, 0);


            int i = 4;
            int j = i;

            if (settings != null)
            {
                i = settings.BiomeSize;
                j = settings.RiverSize;
            }

            if (worldType == WorldType.LARGE_BIOMES)
            {
                i = 6;
            }

            i = getModdedBiomeSize(p_180781_2_, i);

            GenLayer lvt_7_1_ = GenLayerZoom.magnify(1000L, genlayer4, 0);
            GenLayer genlayerriverinit = new GenLayerRiverInit(100L, lvt_7_1_);
            GenLayer genlayerbiomeedge = p_180781_2_.getBiomeLayer(seed, genlayer4, settings);
            GenLayer lvt_9_1_ = GenLayerZoom.magnify(1000L, genlayerriverinit, 2);
            GenLayer genlayerhills = new GenLayerHills(1000L, genlayerbiomeedge, lvt_9_1_);
            GenLayer genlayer5 = GenLayerZoom.magnify(1000L, genlayerriverinit, 2);
            genlayer5 = GenLayerZoom.magnify(1000L, genlayer5, j);
            GenLayer genlayerriver = new GenLayerRiver(1L, genlayer5);
            GenLayer genlayersmooth = new GenLayerSmooth(1000L, genlayerriver);
            genlayerhills = new GenLayerRareBiome(1001L, genlayerhills);

            for (int k = 0; k < i; ++k)
            {
                genlayerhills = new GenLayerZoom((long)(1000 + k), genlayerhills);

                if (k == 0)
                {
                    genlayerhills = new GenLayerAddIsland(3L, genlayerhills);
                }

                if (k == 1 || i == 1)
                {
                    genlayerhills = new GenLayerShore(1000L, genlayerhills);
                }
            }

            GenLayer genlayersmooth1 = new GenLayerSmooth(1000L, genlayerhills);
            GenLayer genlayerrivermix = new GenLayerRiverMix(100L, genlayersmooth1, genlayersmooth);
            GenLayer genlayer3 = new GenLayerVoronoiZoom(10L, genlayerrivermix);
            genlayerrivermix.initWorldGenSeed(seed);
            genlayer3.initWorldGenSeed(seed);
            */
            // TODO
            return new GenLayer[1];
        }
    }
}