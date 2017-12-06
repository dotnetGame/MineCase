using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    public interface IUser : IGrainWithGuidKey
    {
        Task<String> GetName();

        Task SetName(string name);

        Task<uint> GetProtocolVersion();

        Task SetProtocolVersion(uint version);

        Task<IWorld> GetWorld();

        Task<IGameSession> GetGameSession();

        Task<IPlayer> GetPlayer();

        Task Kick();

        Task SetClientPacketSink(IClientboundPacketSink sink);

        Task<IClientboundPacketSink> GetClientPacketSink();

        Task JoinGame();

        Task NotifyLoggedIn();

        Task UpdatePlayerList(IReadOnlyList<IPlayer> players);

        Task SendChatMessage(Chat jsonData, Byte position);

        [OneWay]
        Task OnGameTick(GameTickArgs e);

        Task SetPacketRouter(IPacketRouter packetRouter);

        Task<Slot[]> GetInventorySlots();

        [OneWay]
        Task ForwardPacket(UncompressedPacket packet);

        Task SetInventorySlot(int index, Slot slot);

        Task<GameMode> GetGameMode();

        Task SetGameMode(GameMode gameMode);
    }
}
