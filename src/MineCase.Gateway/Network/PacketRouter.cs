using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Gateway.Network
{
    internal partial class PacketRouter
    {
        private ClientSession _session;
        private SessionState _state;
        private uint _protocolVersion;

        public PacketRouter(ClientSession session)
        {
            _session = session;
        }

        public Task DispatchPacket(UncompressedPacket packet)
        {
            switch (_state)
            {
                case SessionState.Handshaking:
                    return DispatchHandshakingPackets(packet);
                case SessionState.Status:
                    return DispatchStatusPackets(packet);
                case SessionState.Login:
                    return DispatchLoginPackets(packet);
                case SessionState.Play:
                    return DispatchPlayPackets(packet);
                case SessionState.Closed:
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        // public async Task Close()
        // {
        //    _state = SessionState.Closed;
        //    await GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()).Close();
        //    DeactivateOnIdle();
        // }
        public Task Play()
        {
            _state = SessionState.Play;
            return Task.CompletedTask;
        }

        private sealed class DeferredPacketMark
        {
        }

        public enum SessionState
        {
            /// <summary> Hand shaking stage is the first step of client login.</summary>
            Handshaking,

            /// <summary> Clients ping and get server motd before login.</summary>
            Status,

            /// <summary> login stage.</summary>
            Login,

            /// <summary> Session switches to PlayState when players are playing game.</summary>
            Play,

            /// <summary> Clients Disconnection.</summary>
            Closed
        }
    }
}
