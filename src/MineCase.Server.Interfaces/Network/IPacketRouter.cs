using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Network
{
    public interface IPacketRouter : IGrainWithGuidKey
    {
        Task BindToUser(IUser user);

        Task SendPacket(UncompressedPacket packet);

        Task Close();

        Task Play();

        Task OnGameTick();
    }
}
