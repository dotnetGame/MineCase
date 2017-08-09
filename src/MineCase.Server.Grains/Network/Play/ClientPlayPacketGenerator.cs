using MineCase.Protocol.Play;
using MineCase.Server.Game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Play
{
    class ClientPlayPacketGenerator
    {
        public IClientboundPacketSink Sink { get; }

        public ClientPlayPacketGenerator(IClientboundPacketSink sink)
        {
            Sink = sink;
        }

        public Task JoinGame(uint eid, GameMode gameMode, Dimension dimension, Difficulty difficulty, byte maxPlayers, string levelType, bool reducedDebugInfo)
        {
            return Sink.SendPacket(new JoinGame
            {
                EID = (int)eid,
                GameMode = (byte)(((uint)gameMode.ModeClass) | (gameMode.IsHardcore ? 0b100u : 0u)),
                Dimension = (int)dimension,
                Difficulty = (byte)difficulty,
                LevelType = levelType,
                MaxPlayers = maxPlayers,
                ReducedDebugInfo = reducedDebugInfo
            });
        }

        public Task KeepAlive(uint id)
        {
            return Sink.SendPacket(new ClientboundKeepAlive
            {
                KeepAliveId = id
            });
        }
    }
}
