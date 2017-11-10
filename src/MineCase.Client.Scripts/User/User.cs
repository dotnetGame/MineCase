using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Network.Play;
using MineCase.Engine;
using MineCase.Protocol;

namespace MineCase.Client.User
{
    public interface IUser
    {
        Guid UUID { get; }

        string UserName { get; }

        void ForwardPacket(UncompressedPacket packet);
    }

    internal class User : IUser
    {
        public Guid UUID { get; }

        public string UserName { get; }

        private readonly IEventAggregator _eventAggregator;

        public User(Guid uuid, string userName, IEventAggregator eventAggregator)
        {
            UUID = uuid;
            UserName = userName;
            _eventAggregator = eventAggregator;
        }

        public void ForwardPacket(UncompressedPacket packet)
        {
            _eventAggregator.PublishOnCurrentThread(new ClientboundPacketMessage { Packet = packet });
        }
    }
}
