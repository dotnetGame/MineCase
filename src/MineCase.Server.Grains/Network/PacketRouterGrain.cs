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
    /// <summary>
    /// Packet router grain. It send different packet to partial class by its session state.
    /// </summary>
    [Reentrant]
    internal partial class PacketRouterGrain : Grain, IPacketRouter
    {
        private SessionState _state;
        private uint _protocolVersion;
        private string _userName;
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
            if (_user != null)
                await _user.Kick();
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

        public Task<IUser> GetUser()
        {
            return Task.FromResult(_user);
        }

        public Task SetUserName(string name)
        {
            _userName = name;
            return Task.CompletedTask;
        }

        public Task<string> GetUserName()
        {
            return Task.FromResult(_userName);
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
