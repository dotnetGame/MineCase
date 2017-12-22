using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class KeepAliveComponent : Component, IHandle<BeginLogin>, IHandle<PlayerLoggedIn>, IHandle<KickPlayer>
    {
        private uint _keepAliveId = 0;
        public readonly Dictionary<uint, DateTime> _keepAliveWaiters = new Dictionary<uint, DateTime>();
        private bool _isOnline = false;

        private const int ClientKeepInterval = 6;

        public uint Ping { get; private set; }

        public KeepAliveComponent(string name = "keepAlive")
            : base(name)
        {
        }

        public Task ReceiveResponse(uint keepAliveId)
        {
            if (_keepAliveWaiters.TryGetValue(keepAliveId, out var sendTime))
            {
                _keepAliveWaiters.Remove(keepAliveId);
                Ping = (uint)(DateTime.UtcNow - sendTime).TotalMilliseconds;
            }

            return Task.CompletedTask;
        }

        private async Task OnGameTick(object sender, GameTickArgs e)
        {
            if (_isOnline && _keepAliveWaiters.Count >= ClientKeepInterval)
            {
                _isOnline = false;
                await AttachedObject.Tell(new KickPlayer());
            }
            else if (_isOnline && e.WorldAge % 20 == 0)
            {
                var id = _keepAliveId++;
                _keepAliveWaiters.Add(id, DateTime.UtcNow);
                await AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator().KeepAlive(id);
            }
        }

        Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            _keepAliveWaiters.Clear();
            _isOnline = true;
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick += OnGameTick;
            return Task.CompletedTask;
        }

        Task IHandle<KickPlayer>.Handle(KickPlayer message)
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick -= OnGameTick;
            _isOnline = false;
            return Task.CompletedTask;
        }

        Task IHandle<BeginLogin>.Handle(BeginLogin message)
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick -= OnGameTick;
            _isOnline = false;
            return Task.CompletedTask;
        }
    }
}
