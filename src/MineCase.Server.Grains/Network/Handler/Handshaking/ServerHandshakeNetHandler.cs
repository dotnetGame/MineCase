using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol;
using MineCase.Protocol.Protocol.Handshaking.Server;
using Orleans;

namespace MineCase.Server.Network.Handler.Handshaking
{
    public class ServerHandshakeNetHandler : IHandshakeNetHandler
    {
        private IPacketRouter _clientSession;

        private IClientboundPacketSink _packetSink;

        private IGrainFactory _client;

        public ServerHandshakeNetHandler(IPacketRouter session, IClientboundPacketSink packetSink, IGrainFactory client)
        {
            _clientSession = session;
            _packetSink = packetSink;
            _client = client;
        }

        public async Task ProcessHandshake(Handshake packet)
        {
            if (packet.NextState == (int)SessionState.Login)
            {
                await _clientSession.SetSessionState(SessionState.Login);
                if (packet.ProtocolVersion > Protocol.Protocol.Protocol.Version)
                {
                    // ITextComponent itextcomponent = new TranslationTextComponent("multiplayer.disconnect.outdated_server", SharedConstants.getVersion().getName());
                    // this.networkManager.sendPacket(new SDisconnectLoginPacket(itextcomponent));
                    await _packetSink.Close();
                }
                else if (packet.ProtocolVersion > Protocol.Protocol.Protocol.Version)
                {
                    // ITextComponent itextcomponent1 = new TranslationTextComponent("multiplayer.disconnect.outdated_client", SharedConstants.getVersion().getName());
                    // this.networkManager.sendPacket(new SDisconnectLoginPacket(itextcomponent1));
                    await _packetSink.Close();
                }
                else
                {
                    await _clientSession.SetNetHandler(SessionState.Login);
                }
            }
            else if (packet.NextState == (int)SessionState.Status)
            {
                await _clientSession.SetSessionState(SessionState.Status);
                await _clientSession.SetNetHandler(SessionState.Status);
            }
            else
            {
                throw new NotImplementedException("Invalid intention " + packet.NextState.ToString());
            }
        }
    }
}
