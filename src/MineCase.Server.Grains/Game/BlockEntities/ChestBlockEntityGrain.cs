using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.BlockEntities
{
    internal class ChestBlockEntityGrain : BlockEntityGrain, IChestBlockEntity
    {
        public const int ChestSlotsCount = 9 * 3;

        private Slot[] _slots;

        public override Task OnActivateAsync()
        {
            _slots = Enumerable.Repeat(Slot.Empty, ChestSlotsCount).ToArray();
            return base.OnActivateAsync();
        }
    }
}
