using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.World
{
    /// <summary>
    /// 该类一般用于实现大型的世界结构
    /// </summary>
    public abstract class MapGenerator
    {
        /** The number of Chunks to gen-check in any given direction. */
        protected int _range;
        /** The RNG used by the MapGen classes. */
        protected Random _rand;
        /** This world object. */
        protected IWorld _world;

        public MapGenerator(IWorld world, int range = 8)
        {
            _range = range;
            _rand = null;
            _world = world;
        }

        public async Task Generate(IWorld world, int chunkX, int chunkZ, ChunkColumnStorage chunk)
        {
            int range = _range;
            _world = world;
            _rand = new Random(await world.GetSeed());
            int rand1 = _rand.Next();
            int rand2 = _rand.Next();

            // 遍历周围(range*2+1)*(range*2+1)的区块，默认range=8
            for (int x = chunkX - range; x <= chunkX + range; ++x)
            {
                for (int z = chunkZ - range; z <= chunkZ + range; ++z)
                {
                    int randX = x * rand1;
                    int randZ = z * rand2;
                    _rand = new Random(randX ^ randZ ^ await world.GetSeed());

                    // 调用子类方法
                    RecursiveGenerate(world, x, z, chunkX, chunkZ, chunk);
                }
            }
        }

        protected abstract void RecursiveGenerate(IWorld worldIn, int chunkX, int chunkZ, int centerChunkX, int centerChunkZ, ChunkColumnStorage chunk);
    }
}
