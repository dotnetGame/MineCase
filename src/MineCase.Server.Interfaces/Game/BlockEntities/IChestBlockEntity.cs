using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;

namespace MineCase.Server.Game.BlockEntities
{
    [BlockEntity(BlockId.Chest)]
    public interface IChestBlockEntity : IBlockEntity
    {
        Task<Slot> GetSlot(int slotIndex);

        Task SetSlot(int slotIndex, Slot item);

        Task UseBy(IPlayer player);
    }
}
