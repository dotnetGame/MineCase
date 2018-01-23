using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    public interface IDropBlockMapping : IGrainWithIntegerKey
    {
        Task<uint> DropBlock(uint itemId, BlockState block);
    }
}
