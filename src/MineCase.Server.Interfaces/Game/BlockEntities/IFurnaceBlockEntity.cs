using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.Entities;

namespace MineCase.Server.Game.BlockEntities
{
    [BlockEntity(BlockId.Furnace)]
    [BlockEntity(BlockId.BlastFurnace)]
    public interface IFurnaceBlockEntity : IBlockEntity
    {
    }
}
