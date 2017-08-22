﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.User
{
    public interface IUser : IGrainWithGuidKey
    {
        Task SetName(string name);

        Task<uint> GetProtocolVersion();

        Task SetProtocolVersion(uint version);

        Task<IWorld> GetWorld();

        Task<IGameSession> GetGameSession();

        Task<IPlayer> GetPlayer();

        Task SetClientPacketSink(IClientboundPacketSink sink);

        Task<IClientboundPacketSink> GetClientPacketSink();

        Task JoinGame();

        Task NotifyLoggedIn();

        Task KeepAlive(uint keepAliveId);

        Task<uint> GetPing();

        Task OnGameTick(TimeSpan deltaTime);
    }
}
