using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using Orleans;

namespace MineCase.Server.Network
{
    public interface IPacketRouter : IGrainWithGuidKey
    {
        Task SendPacket(UncompressedPacket packet);

        Task Close();

        Task Play();
    }
}
