using Orleans;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Gateway.Network
{
    public class ClientSession : IDisposable
    {
        private TcpClient _tcpClient = null;
        private IClusterClient _client = null;
        private NetworkStream _dataStream = null;
        private bool disposed = false;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _tcpClient = tcpClient;
            _client = clusterClient;
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

        public async Task Startup()
        {
            using (_dataStream = _tcpClient.GetStream())
            {
            }
        }
    }
}
