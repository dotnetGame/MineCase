using MineCase.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineCase.Gateway.Network
{
    class ClientSession : IDisposable
    {
        private readonly TcpClient _tcpClient;
        private bool _useCompression = false;

        public ClientSession(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            using (var remoteStream = _tcpClient.GetStream())
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await DispatchIncommingPacket(remoteStream);
                }
            }
        }

        private async Task DispatchIncommingPacket(Stream remoteStream)
        {
            if(_useCompression)
            {
                var packet = await CompressedPacket.DeserializeAsync(remoteStream);
            }
            else
            {
                var packet = await UncompressedPacket.DeserializeAsync(remoteStream);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tcpClient.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
