using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Generation;

namespace MineCase.Server.World.Generation
{
    public interface IChunkGenerator
    {
        Task<ChunkColumnCompactStorage> Generate(IWorld world, int x, int z, GeneratorSettings settings);

        Task Populate(IWorld world, int x, int z, GeneratorSettings settings);
    }
}
