using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using MineCase.Server.World;
using MineCase.Server.Game.Entity;
using MineCase.World;
using Orleans;

namespace MineCase.Server.User
{
    public interface IUser : IGrainWithStringKey, IGrain
    {
        Task<string> GetName();

        Task Login(Guid sessionId);

        Task Logout();

        Task<uint> GetProtocolVersion();

        Task SetProtocolVersion(uint version);

        Task<IClientboundPacketSink> GetClientPacketSink();

        Task SetClientPacketSink(IClientboundPacketSink sink);

        Task<IPlayer> GetPlayer();

        Task UpdatePlayerList(IReadOnlyList<IPlayer> desc);

        Task RemovePlayerList(IReadOnlyList<IPlayer> desc);
    }
}
