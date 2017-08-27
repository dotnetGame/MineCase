﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Handshaking;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network
{
    internal class ClientboundPacketSinkGrain : Grain, IClientboundPacketSink
    {
        private ObserverSubscriptionManager<IClientboundPacketObserver> _subsManager;
        private readonly IPacketPackager _packetPackager;

        public ClientboundPacketSinkGrain(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public override Task OnActivateAsync()
        {
            _subsManager = new ObserverSubscriptionManager<IClientboundPacketObserver>();
            return base.OnActivateAsync();
        }

        // Clients call this to subscribe.
        public Task Subscribe(IClientboundPacketObserver observer)
        {
            _subsManager.Subscribe(observer);
            return Task.CompletedTask;
        }

        // Also clients use this to unsubscribe themselves to no longer receive the messages.
        public Task UnSubscribe(IClientboundPacketObserver observer)
        {
            _subsManager.Unsubscribe(observer);
            return Task.CompletedTask;
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            var prepared = await _packetPackager.PreparePacket(packet);
            await SendPacket(prepared.packetId, prepared.data.AsImmutable());
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = data.Value
            };
            _subsManager.Notify(n => n.ReceivePacket(packet));
            return Task.CompletedTask;
        }

        public Task Close()
        {
            _subsManager.Notify(n => n.OnClosed());
            DeactivateOnIdle();
            return Task.CompletedTask;
        }
    }
}