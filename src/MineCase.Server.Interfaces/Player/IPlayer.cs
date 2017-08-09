using MineCase.Server.Game;
using MineCase.Server.Network;
using MineCase.Server.World;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Player
{
    public interface IPlayer : IGrainWithGuidKey
    {
        Task<IWorld> GetWorld();
        Task SetClientPacketSink(IClientboundPacketSink sink);
        Task<IClientboundPacketSink> GetClientPacketSink();
    }
}
