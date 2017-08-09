using MineCase.Protocol;
using MineCase.Server.Player;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network
{
    public interface IPacketRouter : IGrainWithGuidKey
    {
        Task BindToPlayer(IPlayer player);
        Task SendPacket(UncompressedPacket packet);
        Task Close();
        Task Play();
    }
}
