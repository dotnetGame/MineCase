using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol;
using Orleans;

namespace MineCase.Server.Network
{
    public interface IPacketRouter : IGrainWithGuidKey
    {
        Task SetSessionState(SessionState state);

        Task SetNetHandler(SessionState state);

        Task ProcessPacket(RawPacket rawPacket);
    }
}
