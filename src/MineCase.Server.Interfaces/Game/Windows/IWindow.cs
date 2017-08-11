using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Windows
{
    public interface IWindow : IGrainWithGuidKey
    {
        Task<uint> GetSlotCount();
        Task<IReadOnlyList<Slot>> GetSlots();
    }
}
