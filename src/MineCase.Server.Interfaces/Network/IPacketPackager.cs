using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Server.Network
{
    public interface IPacketPackager
    {
        Task<(uint PacketId, byte[] Data)> PreparePacket(IPacket packet);
    }
}
