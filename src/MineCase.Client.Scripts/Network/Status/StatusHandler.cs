using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Status;

namespace MineCase.Client.Network.Status
{
    public interface IStatusHandler
    {
        void OnResponse(Response response);

        void OnPong(Pong pong);
    }

    public interface IStatusPacketGenerator
    {
        Task Ping(long payload);

        Task<string> Request();
    }

    internal class StatusHandler : IStatusHandler, IStatusPacketGenerator
    {
        private readonly IPacketSink _packetSink;
        private readonly ConcurrentDictionary<long, TaskCompletionSource<object>> _pendingPings = new ConcurrentDictionary<long, TaskCompletionSource<object>>();
        private readonly ConcurrentQueue<TaskCompletionSource<string>> _pendingRequests = new ConcurrentQueue<TaskCompletionSource<string>>();

        public StatusHandler(IPacketSink packetSink)
        {
            _packetSink = packetSink;
        }

        void IStatusHandler.OnPong(Pong pong)
        {
            if (_pendingPings.TryRemove(pong.Payload, out var tcs))
                tcs.SetResult(null);
        }

        void IStatusHandler.OnResponse(Response response)
        {
            if (_pendingRequests.TryDequeue(out var tcs))
                tcs.SetResult(response.JsonResponse);
        }

        async Task IStatusPacketGenerator.Ping(long payload)
        {
            var tcs = new TaskCompletionSource<object>();
            _pendingPings[payload] = tcs;
            await _packetSink.SendPacket(new Ping { Payload = payload });
            await tcs.Task;
        }

        async Task<string> IStatusPacketGenerator.Request()
        {
            var tcs = new TaskCompletionSource<string>();
            _pendingRequests.Enqueue(tcs);
            await _packetSink.SendPacket(Request.Empty);
            return await tcs.Task;
        }
    }
}
