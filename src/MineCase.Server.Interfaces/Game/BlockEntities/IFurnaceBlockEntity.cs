using System.Threading.Tasks;
using MineCase.Server.Game.Entities;

namespace MineCase.Server.Game.BlockEntities
{
    [BlockEntity(BlockId.Furnace)]
    public interface IFurnaceBlockEntity : IBlockEntity, ITickable
    {
        Task<Slot> GetSlot(int slotIndex);

        Task SetSlot(int slotIndex, Slot item);

        Task UseBy(IPlayer player);
    }
}
