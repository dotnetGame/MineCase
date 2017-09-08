using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.EntityMetadata;
using MineCase.Server.World;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    internal struct ClientPlayPacketGenerator
    {
        public IPacketSink Sink { get; }

        public ClientPlayPacketGenerator(IPacketSink sink)
        {
            Sink = sink;
        }

        public Task SpawnObject(uint entityId, Guid uuid, byte objectType, Vector3 position, float pitch, float yaw, int data)
        {
            return Sink.SendPacket(new SpawnObject
            {
                EID = entityId,
                ObjectUUID = uuid,
                Type = objectType,
                X = position.X,
                Y = position.Y,
                Z = position.Z,
                Pitch = 0,
                Yaw = 0,
                Data = data
            });
        }

        public Task SetSlot(byte windowId, short slot, Slot slotData)
        {
            return Sink.SendPacket(new SetSlot
            {
                WindowId = windowId,
                Slot = slot,
                SlotData = slotData
            });
        }

        public Task DestroyEntities(uint[] entityIds)
        {
            return Sink.SendPacket(new DestroyEntities
            {
                Count = (uint)entityIds.Length,
                EntityIds = entityIds
            });
        }

        public Task EntityMetadata(uint entityId, Entity metadata)
        {
            return Sink.SendPacket(CreateEntityMetadata(entityId, metadata, bw =>
            {
                WriteEntityMetadata(bw, metadata);
            }));
        }

        public Task EntityMetadata(uint entityId, Pickup metadata)
        {
            return Sink.SendPacket(CreateEntityMetadata(entityId, metadata, bw =>
            {
                WriteEntityMetadata(bw, (Entity)metadata);
                WriteEntityMetadata(bw, metadata);
            }));
        }

        private static void WriteEntityMetadata(BinaryWriter bw, Entity metadata)
        {
            byte flag = 0;
            {
                if (metadata.OnFire)
                    flag |= 0x01;
                if (metadata.Crouched)
                    flag |= 0x02;
                if (metadata.Sprinting)
                    flag |= 0x08;
                if (metadata.Invisible)
                    flag |= 0x20;
                if (metadata.GlowingEffect)
                    flag |= 0x40;
                if (metadata.FlyingWithElytra)
                    flag |= 0x80;
            }

            // Flag
            bw.WriteAsEntityMetadata(0, EntityMetadataType.Byte).WriteAsByte(flag);

            // Air
            bw.WriteAsEntityMetadata(1, EntityMetadataType.VarInt).WriteAsVarInt(metadata.Air, out _);

            // Custom name
            bw.WriteAsEntityMetadata(2, EntityMetadataType.String).WriteAsString(metadata.CustomName);

            // Is custom name visible
            bw.WriteAsEntityMetadata(3, EntityMetadataType.Boolean).WriteAsBoolean(metadata.IsCustomNameVisible);

            // Is silent
            bw.WriteAsEntityMetadata(4, EntityMetadataType.Boolean).WriteAsBoolean(metadata.IsSilent);

            // No gravity
            bw.WriteAsEntityMetadata(5, EntityMetadataType.Boolean).WriteAsBoolean(metadata.NoGravity);
        }

        private static void WriteEntityMetadata(BinaryWriter bw, Pickup metadata)
        {
            // Item
            bw.WriteAsEntityMetadata(6, EntityMetadataType.Slot).WriteAsSlot(metadata.Item);
        }

        private static EntityMetadata CreateEntityMetadata<T>(uint entityId, T metadata, Action<BinaryWriter> action)
        {
            using (var stream = new MemoryStream())
            {
                using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    action(bw);
                    bw.WriteAsByte(0xFF);
                }

                return new EntityMetadata { EntityId = entityId, Metadata = stream.ToArray() };
            }
        }

        public Task JoinGame(uint eid, GameMode gameMode, Dimension dimension, Difficulty difficulty, byte maxPlayers, string levelType, bool reducedDebugInfo)
        {
            return Sink.SendPacket(new JoinGame
            {
                EID = (int)eid,
                GameMode = ToByte(gameMode),
                Dimension = (int)dimension,
                Difficulty = (byte)difficulty,
                LevelType = levelType,
                MaxPlayers = maxPlayers,
                ReducedDebugInfo = reducedDebugInfo
            });
        }

        public Task KeepAlive(uint id)
        {
            return Sink.SendPacket(new ClientboundKeepAlive
            {
                KeepAliveId = id
            });
        }

        public Task UpdateHealth(uint health, uint maxHealth, uint food, uint maxFood, float foodSaturation)
        {
            return Sink.SendPacket(new UpdateHealth
            {
                Health = (float)health / maxHealth * 20,
                Food = (uint)((float)food / maxFood * 20),
                FoodSaturation = foodSaturation
            });
        }

        public Task SetExperience(float experienceBar, uint level, uint totalExp)
        {
            return Sink.SendPacket(new SetExperience
            {
                ExperienceBar = experienceBar,
                Level = level,
                TotalExperience = totalExp
            });
        }

        public Task TimeUpdate(long age, long timeOfDay)
        {
            return Sink.SendPacket(new TimeUpdate
            {
                WorldAge = age,
                TimeOfDay = timeOfDay
            });
        }

        public Task PlayerListItemAddPlayer(IReadOnlyList<PlayerDescription> desc)
        {
            return Sink.SendPacket(new PlayerListItem<PlayerListItemAddPlayerAction>
            {
                Action = 0,
                NumberOfPlayers = (uint)desc.Count,
                Players = (from d in desc
                           select new PlayerListItemAddPlayerAction
                           {
                               UUID = d.UUID,
                               Name = d.Name,
                               NumberOfProperties = 0,
                               GameMode = ToByte(d.GameMode),
                               Ping = d.Ping,
                               HasDisplayName = false
                           }).ToArray()
            });
        }

        public Task WindowItems(byte windowId, IReadOnlyList<Slot> slots)
        {
            return Sink.SendPacket(new WindowItems
            {
                WindowId = windowId,
                Count = (short)slots.Count,
                Slots = slots.ToArray()
            });
        }

        public Task PositionAndLook(double x, double y, double z, float yaw, float pitch, RelativeFlags relative, uint teleportId)
        {
            return Sink.SendPacket(new ClientboundPositionAndLook
            {
                X = x,
                Y = y,
                Z = z,
                Yaw = yaw,
                Pitch = pitch,
                Flags = (byte)relative,
                TeleportId = teleportId
            });
        }

        public Task SendChatMessage(Chat jsonData, Byte position)
        {
            return Sink.SendPacket(new ClientboundChatMessage
            {
                JSONData = jsonData,
                Position = position
            });
        }

        public Task CollectItem(uint collectedEntityId, uint entityId, uint itemCount)
        {
            return Sink.SendPacket(new CollectItem
            {
                CollectedEntityId = collectedEntityId,
                CollectorEntityId = entityId,
                PickupItemCount = itemCount
            });
        }

        public Task ChunkData(Dimension dimension, int chunkX, int chunkZ, ChunkColumnStorage chunkColumn)
        {
            return Sink.SendPacket(new ChunkData
            {
                ChunkX = chunkX,
                ChunkZ = chunkZ,
                GroundUpContinuous = chunkColumn.Biomes != null,
                Biomes = chunkColumn.Biomes,
                PrimaryBitMask = chunkColumn.SectionBitMask,
                NumberOfBlockEntities = 0,
                Data = (from c in chunkColumn.Sections
                        where c != null
                        select new Protocol.Play.ChunkSection
                        {
                            PaletteLength = 0,
                            BitsPerBlock = c.BitsPerBlock,
                            SkyLight = c.SkyLight.Storage,
                            BlockLight = c.BlockLight.Storage,
                            DataArray = c.Data.Storage
                        }).ToArray()
            });
        }

        public Task SendPacket(uint packetId, byte[] data)
        {
            return Sink.SendPacket(packetId, data.AsImmutable());
        }

        public static byte ToByte(GameMode gameMode)
        {
            return (byte)(((uint)gameMode.ModeClass) | (gameMode.IsHardcore ? 0b100u : 0u));
        }

        public Task UnloadChunk(int chunkX, int chunkZ)
        {
            return Sink.SendPacket(new UnloadChunk
            {
                ChunkX = chunkX,
                ChunkZ = chunkZ
            });
        }

        public Task SendClientAnimation(uint entityID, ClientboundAnimationId animationID)
        {
            return Sink.SendPacket(new ClientboundAnimation
            {
                EntityID = entityID,
                AnimationID = animationID
            });
        }

        public Task BlockChange(Position location, BlockState blockState)
        {
            return Sink.SendPacket(new BlockChange
            {
                Location = location,
                BlockId = blockState.ToUInt32()
            });
        }

        public Task ConfirmTransaction(byte windowId, short actionNumber, bool accepted)
        {
            return Sink.SendPacket(new ClientboundConfirmTransaction
            {
                WindowId = windowId,
                ActionNumber = actionNumber,
                Accepted = accepted
            });
        }

        public Task OpenWindow(byte windowId, string windowType, Chat windowTitle, byte numberOfSlots, byte? entityId)
        {
            return Sink.SendPacket(new OpenWindow
            {
                WindowId = windowId,
                WindowType = windowType,
                WindowTitle = windowTitle,
                NumberOfSlots = numberOfSlots,
                EntityId = entityId
            });
        }

        public Task CloseWindow(byte windowId)
        {
            return Sink.SendPacket(new ClientboundCloseWindow
            {
                WindowId = windowId
            });
        }
    }

    [Flags]
    public enum RelativeFlags : byte
    {
        X = 0x1,
        Y = 0x2,
        Z = 0x4,
        Yaw = 0x8,
        Pitch = 0x10
    }
}
