using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Gateway.Network
{
    public class ClientSession : IDisposable
    {
        private readonly Guid _sessionId;
        private TcpClient _tcpClient = null;
        private IClusterClient _client = null;
        private NetworkStream _dataStream = null;

        private volatile bool _useCompression = false;
        private uint _compressThreshold;

        private bool disposed = false;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _client = clusterClient;

            // _outcomingPacketDispatcher = new ActionBlock<UncompressedPacket>(SendOutcomingPacket);
            // _outcomingPacketObserver = new OutcomingPacketObserver(this);
        }

        ~ClientSession()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_tcpClient != null)
                {
                    _tcpClient.Dispose();
                    _tcpClient = null;
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        public void OnClosed()
        {
        }

        public async Task Startup()
        {
            using (_dataStream = _tcpClient.GetStream())
            {
                try
                {
                    while (true)
                    {
                        await PreprocessPacket();
                    }
                }
                catch (EndOfStreamException)
                {
                }
            }
        }

        private async Task PreprocessPacket()
        {
        }
    }
}
