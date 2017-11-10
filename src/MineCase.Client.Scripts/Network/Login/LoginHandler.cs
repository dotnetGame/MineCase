using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.User;
using MineCase.Protocol.Login;

namespace MineCase.Client.Network.Login
{
    public interface ILoginHandler
    {
        void OnDisconnect(LoginDisconnect disconnect);

        void OnLoginSuccess(LoginSuccess loginSuccess);
    }

    public abstract class LoginStartResponse
    {
    }

    public sealed class LoginStartDisconnectResponse : LoginStartResponse
    {
        public string Reason { get; set; }
    }

    public sealed class LoginStartSuccessResponse : LoginStartResponse
    {
        public IUser User { get; set; }
    }

    public interface ILoginPacketGenerator
    {
        Task<LoginStartResponse> LoginStart(string name);
    }

    internal class LoginHandler : ILoginHandler, ILoginPacketGenerator
    {
        private readonly IPacketSink _packetSink;
        private readonly IPacketRouter _packetRouter;
        private readonly ConcurrentQueue<TaskCompletionSource<LoginStartResponse>> _pendingLogins = new ConcurrentQueue<TaskCompletionSource<LoginStartResponse>>();

        public LoginHandler(IPacketSink packetSink, IPacketRouter packetRouter)
        {
            _packetSink = packetSink;
            _packetRouter = packetRouter;
        }

        void ILoginHandler.OnLoginSuccess(LoginSuccess loginSuccess)
        {
            if (_pendingLogins.TryDequeue(out var tcs))
            {
                var user = new User.User(Guid.Parse(loginSuccess.UUID), loginSuccess.Username);
                _packetRouter.BindToUser(user);
                tcs.SetResult(new LoginStartSuccessResponse { User = user });
            }
        }

        void ILoginHandler.OnDisconnect(LoginDisconnect disconnect)
        {
            if (_pendingLogins.TryDequeue(out var tcs))
            {
                tcs.SetResult(new LoginStartDisconnectResponse
                {
                    Reason = disconnect.Reason
                });
            }
        }

        async Task<LoginStartResponse> ILoginPacketGenerator.LoginStart(string name)
        {
            var tcs = new TaskCompletionSource<LoginStartResponse>();
            _pendingLogins.Enqueue(tcs);
            await _packetSink.SendPacket(new LoginStart { Name = name });
            return await tcs.Task;
        }
    }
}
