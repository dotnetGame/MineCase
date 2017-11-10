using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Messages;
using MineCase.Client.World;
using MineCase.Engine;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.Network.Play
{
    public class ClientboundPacketComponent : SmartBehaviour, IHandle<ClientboundPacketMessage>
    {
        public IEventAggregator EventAggregator { get; set; }

        private Queue<object> _deferredPacket;

        private void Start()
        {
            _deferredPacket = new Queue<object>();
            EventAggregator.Subscribe(this);
        }

        private void FixedUpdate()
        {
            while (_deferredPacket.Count != 0)
                DispatchPacket((dynamic)_deferredPacket.Dequeue());
        }

        private void DispatchPacket(object packet)
        {
            throw new NotImplementedException();
        }

        private object DeserializePlayPacket(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            object innerPacket;
            switch (packet.PacketId)
            {
                // Entity Look And Relative Move
                case 0x27:
                    innerPacket = EntityLookAndRelativeMove.Deserialize(ref br);
                    break;

                // Chunk Data
                case 0x20:
                    innerPacket = ChunkData.Deserialize(ref br, true);
                    break;

                default:
                    // Debug.LogWarning($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
                    return null;
            }

            if (!br.IsCosumed)
                throw new InvalidDataException($"Packet data is not fully consumed.");
            return innerPacket;
        }

        void IHandle<ClientboundPacketMessage>.Handle(ClientboundPacketMessage message)
        {
            var packet = DeserializePlayPacket(message.Packet);
            if (packet != null)
                _deferredPacket.Enqueue(packet);
        }

        private void DispatchPacket(EntityLookAndRelativeMove packet)
        {
        }

        private void DispatchPacket(ChunkData packet)
        {
            var column = new ChunkColumnCompactStorage(packet.Biomes);
            var mask = packet.PrimaryBitMask;
            int index = 0;
            int srcIndex = 0;
            while (index < ChunkConstants.SectionsPerChunk)
            {
                if ((mask & 1) == 1)
                {
                    var src = packet.Data[srcIndex];
                    column.Sections[index] = new ChunkSectionCompactStorage(
                        new ChunkSectionCompactStorage.DataArray(src.DataArray),
                        new ChunkSectionCompactStorage.NibbleArray(src.BlockLight),
                        src.SkyLight != null ? new ChunkSectionCompactStorage.NibbleArray(src.SkyLight) : null);
                    srcIndex++;
                }

                mask >>= 1;
                index++;
            }

            System.Diagnostics.Debug.Assert(packet.PrimaryBitMask == column.SectionBitMask, "PrimaryBitMask must be equal.");
            var loader = FindObjectOfType<UserChunkLoaderComponent>();
            loader.LoadTerrain(packet.ChunkX, packet.ChunkZ, column);
        }
    }

    public sealed class ClientboundPacketMessage
    {
        public UncompressedPacket Packet { get; set; }
    }
}
