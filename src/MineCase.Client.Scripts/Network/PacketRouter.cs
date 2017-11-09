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
    public interface IPacketRouter
    {
        Task SendPacket(UncompressedPacket packet);
    }

    /// <summary>
    /// 包路由
    /// </summary>
    internal partial class PacketRouter : IPacketRouter
    {
        public SessionState State { get; set; }

        private readonly Guid _sessionId;
        private readonly IEventAggregator _eventAggregator;
        private readonly IComponentContext _componentContext;

        public PacketRouter(Guid sessionId, IEventAggregator eventAggregator, IComponentContext componentContext)
        {
            _sessionId = sessionId;
            _eventAggregator = eventAggregator;
            _componentContext = componentContext;
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
                    // innerPacket = DeserializeLoginPacket(packet);
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
    }
}
