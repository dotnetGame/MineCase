using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MineCase.Client.User;
using MineCase.Engine;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    public enum SessionState
    {
        /// <summary>
        /// Status
        /// </summary>
        Status,

        /// <summary>
        /// Login
        /// </summary>
        Login,

        /// <summary>
        /// Play
        /// </summary>
        Play,

        /// <summary>
        /// 关闭
        /// </summary>
        Closed
    }

    public interface IPacketRouter
    {
        SessionState State { get; set; }

        Task SendPacket(UncompressedPacket packet);

        void BindToUser(IUser user);
    }

    /// <summary>
    /// 包路由
    /// </summary>
    internal partial class PacketRouter : IPacketRouter
    {
        public SessionState State { get; set; }

        private readonly SessionScope _sessionScope;
        private readonly IEventAggregator _eventAggregator;
        private IUser _user;

        public PacketRouter(SessionScope sessionScope, IEventAggregator eventAggregator)
        {
            _sessionScope = sessionScope;
            _eventAggregator = eventAggregator;
        }

        public async Task SendPacket(UncompressedPacket packet)
        {
            dynamic innerPacket = new object();
            switch (State)
            {
                case SessionState.Status:
                    innerPacket = DeserializeStatusPacket(packet);
                    break;
                case SessionState.Login:
                    innerPacket = DeserializeLoginPacket(packet);
                    break;
                case SessionState.Play:
                    // await _user.ForwardPacket(packet);
                    return;
                case SessionState.Closed:
                    break;
                default:
                    break;
            }

            await DispatchPacket(innerPacket);
        }

        private Task DispatchPacket(object packet)
        {
            throw new NotImplementedException();
        }

        public void BindToUser(IUser user)
        {
            _user = user;
            State = SessionState.Play;
        }
    }
}
