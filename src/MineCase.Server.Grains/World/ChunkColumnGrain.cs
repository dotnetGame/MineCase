using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World
{
    internal class ChunkColumnGrain : Grain, IChunkColumn
    {
        private IWorld _world;
        private int _chunkX;
        private int _chunkZ;

        public Task<ChunkColumn> GetState()
        {
            return Task.FromResult(new ChunkColumn
            {
                Biomes = Enumerable.Repeat<byte>(0, 256).ToArray(),
                SectionBitMask = 0b1111_1111_1111_1111,
                Sections = Enumerable.Repeat(
                    new ChunkSection
                    {
                        BitsPerBlock = 13,
                        Blocks = Enumerable.Repeat(
                            new Block
                            {
                            }, 16 * 16 * 16).ToArray()
                    }, 16).ToArray()
            });
        }

        public override Task OnActivateAsync()
        {
            var key = this.GetWorldAndChunkPosition();
            _world = GrainFactory.GetGrain<IWorld>(key.worldKey);
            _chunkX = key.x;
            _chunkZ = key.z;

            return base.OnActivateAsync();
        }
    }
}
