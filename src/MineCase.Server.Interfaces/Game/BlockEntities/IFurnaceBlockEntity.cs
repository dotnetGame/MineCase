using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.Entities;

namespace MineCase.Server.Game.BlockEntities
{
    [BlockEntity(BlockId.Furnace)]
    [BlockEntity(BlockId.BurningFurnace)]
    public interface IFurnaceBlockEntity : IBlockEntity
    {
    }
}
