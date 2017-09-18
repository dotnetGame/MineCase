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

        public async Task SendPacket(UncompressedPacket packet)
        {
            dynamic innerPacket = new object();
            switch (_state)
            {
                case SessionState.Handshaking:
                    innerPacket = DeserializeHandshakingPacket(packet);
                    break;
                case SessionState.Status:
                    innerPacket = DeserializeStatusPacket(packet);
                    break;
                case SessionState.Login:
                    innerPacket = DeserializeLoginPacket(packet);
                    break;
                case SessionState.Play:
                    await _user.ForwardPacket(packet);
                    break;
                case SessionState.Closed:
                    break;
                default:
                    break;
            }
        }

        public async Task Close()
        {
            _state = SessionState.Closed;
            await GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()).Close();
            DeactivateOnIdle();
        }

        private Task DispatchPacket(object packet)
        {
            throw new NotImplementedException();
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
