using MineCase.Server.Game;
using MineCase.Server.Network;
using MineCase.Server.World;
using Orleans;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Player
{
    public interface IPlayer : IGrainWithGuidKey, IEntity
    {
        Task<IWorld> GetWorld();
        Task<IGameSession> GetGameSession();
        Task SetClientPacketSink(IClientboundPacketSink sink);
        Task<IClientboundPacketSink> GetClientPacketSink();

        Task KeepAlive(uint keepAliveId);
        Task UseEntity(uint targetEid, EntityUsage type, Vector3? targetPosition, EntityInteractHand? hand);
    }
}
