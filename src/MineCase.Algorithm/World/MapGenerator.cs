using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.World.Biomes;
using MineCase.World;

namespace MineCase.Algorithm.World
{
    public class MapGenerationInfo
    {
        public int Seed { get; set; }
    }

    /// <summary>
    /// 该类一般用于实现大型的世界结构.
    /// </summary>
    public abstract class MapGenerator
    {
        /** The number of Chunks to gen-check in any given direction. */
        protected int _range;
        /** The RNG used by the MapGen classes. */
        protected Random _rand;
        /** This world object. */
        protected MapGenerationInfo _info;

        public MapGenerator(MapGenerationInfo info, int range = 8)
        {
            _range = range;
            _rand = null;
            _info = info;
        }

        public void Generate(MapGenerationInfo info, int chunkX, int chunkZ, ChunkColumnStorage chunk, Biome biome)
        {
            int range = _range;
            _info = info;
            _rand = new Random(info.Seed);
            int rand1 = _rand.Next();
            int rand2 = _rand.Next();

            // 遍历周围(range*2+1)*(range*2+1)的区块，默认range=8
            for (int x = chunkX - range; x <= chunkX + range; ++x)
            {
                for (int z = chunkZ - range; z <= chunkZ + range; ++z)
                {
                    int randX = x * rand1;
                    int randZ = z * rand2;
                    _rand = new Random(randX ^ randZ ^ info.Seed);

                    // 调用子类方法
                    RecursiveGenerate(info, x, z, chunkX, chunkZ, chunk, biome);
                }
            }
        }

        protected abstract void RecursiveGenerate(MapGenerationInfo info, int chunkX, int chunkZ, int centerChunkX, int centerChunkZ, ChunkColumnStorage chunk, Biome biome);
    }
}
