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

        private readonly Queue<object> _deferredPacket = new Queue<object>();
        private readonly object _deferPacketMark = new DeferredPacketMark();

        public async Task SendPacket(UncompressedPacket packet)
        {
            dynamic innerPacket = new object();
            switch (_state)
            {
                case SessionState.Handshaking:
                    innerPacket = DeserializeHandshakingPacket(ref packet);
                    break;
                case SessionState.Status:
                    innerPacket = DeserializeStatusPacket(ref packet);
                    break;
                case SessionState.Login:
                    innerPacket = DeserializeLoginPacket(ref packet);
                    break;
                case SessionState.Play:
                    innerPacket = DeserializePlayPacket(ref packet);
                    break;
                case SessionState.Closed:
                    break;
                default:
                    break;
            }

            if (!(innerPacket is DeferredPacketMark))
                await DispatchPacket(innerPacket);
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

        public async Task OnGameTick()
        {
            while (_deferredPacket.Count != 0)
                await DispatchPacket((dynamic)_deferredPacket.Dequeue());
        }

        protected object DeferPacket(object packet)
        {
            _deferredPacket.Enqueue(packet);
            return _deferPacketMark;
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
