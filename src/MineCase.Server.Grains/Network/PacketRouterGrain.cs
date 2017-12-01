using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Handshaking;
using MineCase.Server.User;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network
{
    [Reentrant]
    internal partial class PacketRouterGrain : Grain, IPacketRouter
    {
        private SessionState _state;
        private uint _protocolVersion;
        private IUser _user;

        public Task SendPacket(UncompressedPacket packet)
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
                    return _user.ForwardPacket(packet);
                case SessionState.Closed:
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        public async Task Close()
        {
            _state = SessionState.Closed;
            await GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()).Close();
            DeactivateOnIdle();
        }

        public Task Play()
        {
            _state = SessionState.Play;
            return Task.CompletedTask;
        }

        public Task BindToUser(IUser user)
        {
            _user = user;
            return Task.CompletedTask;
        }

        private sealed class DeferredPacketMark
        {
        }

        public enum SessionState
        {
            Handshaking,
            Status,
            Login,
            Play,
            Closed
        }
    }
}
