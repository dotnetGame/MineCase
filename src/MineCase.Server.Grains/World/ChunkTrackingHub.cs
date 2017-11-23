using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("chunkTrackingHub")]
    [Reentrant]
    internal class ChunkTrackingHub : AddressByPartitionGrain, IChunkTrackingHub
    {
        private readonly IPacketPackager _packetPackager;
        private Dictionary<IPlayer, IPacketSink> _trackingPlayers;
        private BroadcastPacketSink _broadcastPacketSink;
        private AutoSaveStateComponent _autoSave;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        public ChunkTrackingHub(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        protected override async Task InitializePreLoadComponent()
        {
            var state = new StateComponent<StateHolder>();
            await SetComponent(state);
            state.BeforeWriteState += State_BeforeWriteState;
            state.AfterReadState += State_AfterReadState;
        }

        protected override async Task InitializeComponents()
        {
            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            await SetComponent(_autoSave);
        }

        private Task State_AfterReadState(object sender, EventArgs e)
        {
            _trackingPlayers = (from p in State.Players
                                let sink = (IPacketSink)new ForwardToPlayerPacketSink(p, _packetPackager)
                                select new { Player = p, Sink = sink }).ToDictionary(o => o.Player, o => o.Sink);
            _broadcastPacketSink = new BroadcastPacketSink(_trackingPlayers.Values, _packetPackager);
            return Task.CompletedTask;
        }

        private Task State_BeforeWriteState(object sender, EventArgs e)
        {
            State.Players = _trackingPlayers.Keys.ToList();
            return Task.CompletedTask;
        }

        public Task SendPacket(ISerializablePacket packet)
        {
            return _broadcastPacketSink.SendPacket(packet);
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            return _broadcastPacketSink.SendPacket(packetId, data);
        }

        public Task Subscribe(IPlayer player)
        {
            if (!_trackingPlayers.ContainsKey(player))
            {
                _trackingPlayers.Add(player, new ForwardToPlayerPacketSink(player, _packetPackager));
                MarkDirty();
            }

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IPlayer player)
        {
            if (_trackingPlayers.Remove(player))
                MarkDirty();
            return Task.CompletedTask;
        }

        public Task<List<IPlayer>> GetTrackedPlayers()
        {
            return Task.FromResult(_trackingPlayers.Keys.ToList());
        }

        public Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            return _autoSave.OnGameTick(this, (deltaTime, worldAge));
        }

        private void MarkDirty()
        {
            _autoSave.IsDirty = true;
        }

        internal class StateHolder
        {
            public List<IPlayer> Players { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                Players = new List<IPlayer>();
            }
        }
    }
}
