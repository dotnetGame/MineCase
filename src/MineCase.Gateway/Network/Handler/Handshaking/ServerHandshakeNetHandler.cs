using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Gateway.Network.Handler.Status;
using MineCase.Protocol.Handshaking;
using MineCase.Protocol.Protocol;
using MineCase.Protocol.Protocol.Status.Server;

namespace MineCase.Gateway.Network.Handler.Handshaking
{
    public class ServerHandshakeNetHandler : IHandshakeNetHandler
    {
        private ClientSession _clientSession;

        public ServerHandshakeNetHandler(ClientSession session)
        {
            _clientSession = session;
        }

        public Task ProcessHandshake(Handshake packet)
        {
            if (packet.NextState == (int)SessionState.Login)
            {
                _clientSession.SetSessionState(SessionState.Login);
                if (packet.ProtocolVersion > Protocol.Protocol.Protocol.Version)
                {
                    // ITextComponent itextcomponent = new TranslationTextComponent("multiplayer.disconnect.outdated_server", SharedConstants.getVersion().getName());
                    // this.networkManager.sendPacket(new SDisconnectLoginPacket(itextcomponent));
                    _clientSession.Close();
                }
                else if (packet.ProtocolVersion > Protocol.Protocol.Protocol.Version)
                {
                    // ITextComponent itextcomponent1 = new TranslationTextComponent("multiplayer.disconnect.outdated_client", SharedConstants.getVersion().getName());
                    // this.networkManager.sendPacket(new SDisconnectLoginPacket(itextcomponent1));
                    _clientSession.Close();
                }
                else
                {
                    _clientSession.SetNetHandler(SessionState.Login);
                }
            }
            else if (packet.NextState == (int)SessionState.Status)
            {
                _clientSession.SetSessionState(SessionState.Status);
                _clientSession.SetNetHandler(SessionState.Status);
            }
            else
            {
                throw new NotImplementedException("Invalid intention " + packet.NextState.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
