using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Windows;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.EntityMetadata;
using MineCase.Server.World;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    internal struct ClientPlayPacketGenerator
    {
        public IPacketSink Sink { get; }

        public IBroadcastPacketSink BroadcastSink { get; }

        private IPlayer _except;

        public ClientPlayPacketGenerator(IPacketSink sink)
        {
            Sink = sink;
            BroadcastSink = null;
            _except = null;
        }

        public ClientPlayPacketGenerator(IBroadcastPacketSink sink, IPlayer except)
        {
            Sink = null;
            BroadcastSink = sink;
            _except = except;
        }

        public Task SpawnObject(uint entityId, Guid uuid, byte objectType, Vector3 position, float pitch, float yaw, int data)
        {
            return SendPacket(new SpawnObject
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

        public Task SpawnMob(uint entityId, Guid uuid, byte entityType, Vector3 position, float pitch, float yaw, Game.Entities.EntityMetadata.Entity metadata)
        {
            return SendPacket(CreateSpawnMob(entityId, uuid, entityType, position, pitch, yaw, metadata, bw =>
                 {
                     WriteEntityMetadata(bw, metadata);
                 }));
        }

        public Task SpawnPlayer(uint entityId, Guid uuid, Vector3 position, float pitch, float yaw, Game.Entities.EntityMetadata.Player metadata)
        {
            var metaPacket = CreateEntityMetadata(entityId, metadata, bw =>
            {
                WriteEntityMetadata(bw, (Game.Entities.EntityMetadata.Entity)metadata);
                WriteEntityMetadata(bw, (Game.Entities.EntityMetadata.Living)metadata);
                WriteEntityMetadata(bw, metadata);
            });
            return SendPacket(new SpawnPlayer
            {
                EntityId = entityId,
                PlayerUUID = uuid,
                X = position.X,
                Y = position.Y,
                Z = position.Z,
                Pitch = pitch,
                Yaw = yaw,
                Metadata = metaPacket.Metadata
            });
        }

        public Task SetSlot(byte windowId, short slot, Slot slotData)
        {
            return SendPacket(new SetSlot
            {
                WindowId = windowId,
                Slot = slot,
                SlotData = slotData
            });
        }

        public Task DestroyEntities(uint[] entityIds)
        {
            return SendPacket(new DestroyEntities
            {
                Count = (uint)entityIds.Length,
                EntityIds = entityIds
            });
        }

        public Task EntityMetadata(uint entityId, Game.Entities.EntityMetadata.Entity metadata)
        {
            return SendPacket(CreateEntityMetadata(entityId, metadata, bw =>
            {
                WriteEntityMetadata(bw, metadata);
            }));
        }

        public Task EntityMetadata(uint entityId, Pickup metadata)
        {
            return SendPacket(CreateEntityMetadata(entityId, metadata, bw =>
            {
                WriteEntityMetadata(bw, (Game.Entities.EntityMetadata.Entity)metadata);
                WriteEntityMetadata(bw, metadata);
            }));
        }

        private static void WriteEntityMetadata(BinaryWriter bw, Game.Entities.EntityMetadata.Entity metadata)
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

        private static void WriteEntityMetadata(BinaryWriter bw, Game.Entities.EntityMetadata.Living metadata)
        {
            byte handStates = 0;
            {
                if (metadata.IsHandActive)
                    handStates |= 0x01;
                if (metadata.ActiveHand == SwingHandState.OffHand)
                    handStates |= 0x02;
            }

            // Hnad States
            bw.WriteAsEntityMetadata(6, EntityMetadataType.Byte).WriteAsByte(handStates);

            // Health
            bw.WriteAsEntityMetadata(7, EntityMetadataType.Float).WriteAsFloat(metadata.Health);

            // Potion effect color
            bw.WriteAsEntityMetadata(8, EntityMetadataType.VarInt).WriteAsVarInt(metadata.PotionEffectColor, out _);

            // Is potion effect ambient
            bw.WriteAsEntityMetadata(9, EntityMetadataType.Boolean).WriteAsBoolean(metadata.IsPotionEffectAmbient);

            // Number of arrows in entity
            bw.WriteAsEntityMetadata(10, EntityMetadataType.VarInt).WriteAsVarInt(metadata.NumberOfArrows, out _);
        }

        private static void WriteEntityMetadata(BinaryWriter bw, Game.Entities.EntityMetadata.Player metadata)
        {
            byte skinParts = 0;
            {
                if (metadata.CapeEnabled)
                    skinParts |= 0x01;
                if (metadata.JacketEnabled)
                    skinParts |= 0x02;
                if (metadata.LeftSleeveEnabled)
                    skinParts |= 0x04;
                if (metadata.RightSleeveEnabled)
                    skinParts |= 0x08;
                if (metadata.LeftPantsLegEnabled)
                    skinParts |= 0x10;
                if (metadata.RightPantsLegEnabled)
                    skinParts |= 0x20;
                if (metadata.HatEnabled)
                    skinParts |= 0x40;
            }

            // Additional Hearts
            bw.WriteAsEntityMetadata(11, EntityMetadataType.Float).WriteAsFloat(metadata.AdditionalHearts);

            // Score
            bw.WriteAsEntityMetadata(12, EntityMetadataType.VarInt).WriteAsVarInt(metadata.Score, out _);

            // The Displayed Skin Parts
            bw.WriteAsEntityMetadata(13, EntityMetadataType.Byte).WriteAsByte(skinParts);

            // Main hand
            bw.WriteAsEntityMetadata(14, EntityMetadataType.Byte).WriteAsByte(metadata.MainHand);

            // TODO: And NBT fields
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

        private static SpawnMob CreateSpawnMob<T>(uint entityId, Guid uuid, byte entityType, Vector3 position, float pitch, float yaw, T metadata, Action<BinaryWriter> action)
        {
            using (var stream = new MemoryStream())
            {
                using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    action(bw);
                    bw.WriteAsByte(0xFF);
                }

                return new SpawnMob
                {
                    EID = entityId,
                    EntityUUID = uuid,
                    Type = entityType,
                    X = position.X,
                    Y = position.Y,
                    Z = position.Z,
                    Pitch = 0,
                    Yaw = 0,
                    Metadata = stream.ToArray()
                };
            }
        }

        public Task JoinGame(uint eid, GameMode gameMode, Dimension dimension, Difficulty difficulty, byte maxPlayers, string levelType, bool reducedDebugInfo)
        {
            return SendPacket(new JoinGame
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

        public Task EntityRelativeMove(uint eid, short deltaX, short deltaY, short deltaZ, bool onGround)
        {
            return SendPacket(new EntityRelativeMove
            {
                EID = eid,
                DeltaX = deltaX,
                DeltaY = deltaY,
                DeltaZ = deltaZ,
                OnGround = onGround
            });
        }

        public Task EntityLookAndRelativeMove(uint eid, short deltaX, short deltaY, short deltaZ, byte yaw, byte pitch, bool onGround)
        {
            return SendPacket(new EntityLookAndRelativeMove
            {
                EID = eid,
                DeltaX = deltaX,
                DeltaY = deltaY,
                DeltaZ = deltaZ,
                Yaw = yaw,
                Pitch = pitch,
                OnGround = onGround
            });
        }

        public Task EntityLook(uint eid, byte yaw, byte pitch, bool onGround)
        {
            return SendPacket(new EntityLook
            {
                EID = eid,
                Yaw = yaw,
                Pitch = pitch,
                OnGround = onGround
            });
        }

        public Task EntityHeadLook(uint eid, byte yaw)
        {
            return SendPacket(new EntityHeadLook
            {
                EID = eid,
                Yaw = yaw
            });
        }

        public Task KeepAlive(uint id)
        {
            return SendPacket(new ClientboundKeepAlive
            {
                KeepAliveId = id
            });
        }

        public Task UpdateHealth(uint health, uint maxHealth, uint food, uint maxFood, float foodSaturation)
        {
            return SendPacket(new UpdateHealth
            {
                Health = (float)health / maxHealth * 20,
                Food = (uint)((float)food / maxFood * 20),
                FoodSaturation = foodSaturation
            });
        }

        public Task SetExperience(float experienceBar, uint level, uint totalExp)
        {
            return SendPacket(new SetExperience
            {
                ExperienceBar = experienceBar,
                Level = level,
                TotalExperience = totalExp
            });
        }

        public Task TimeUpdate(long age, long timeOfDay)
        {
            return SendPacket(new TimeUpdate
            {
                WorldAge = age,
                TimeOfDay = timeOfDay
            });
        }

        public Task PlayerListItemAddPlayer(IReadOnlyList<PlayerDescription> desc)
        {
            return SendPacket(new PlayerListItem<PlayerListItemAddPlayerAction>
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

        public Task PlayerListItemRemovePlayer(IReadOnlyList<Guid> desc)
        {
            return SendPacket(new PlayerListItem<PlayerListItemRemovePlayerAction>
            {
                Action = 4,
                NumberOfPlayers = (uint)desc.Count,
                Players = (from d in desc
                           select new PlayerListItemRemovePlayerAction
                           {
                               UUID = d
                           }).ToArray()
            });
        }

        public Task WindowItems(byte windowId, IReadOnlyList<Slot> slots)
        {
            return SendPacket(new WindowItems
            {
                WindowId = windowId,
                Count = (short)slots.Count,
                Slots = slots.ToArray()
            });
        }

        public Task PositionAndLook(double x, double y, double z, float yaw, float pitch, RelativeFlags relative, uint teleportId)
        {
            return SendPacket(new ClientboundPositionAndLook
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
            return SendPacket(new ClientboundChatMessage
            {
                JSONData = jsonData,
                Position = position
            });
        }

        public Task CollectItem(uint collectedEntityId, uint entityId, uint itemCount)
        {
            return SendPacket(new CollectItem
            {
                CollectedEntityId = collectedEntityId,
                CollectorEntityId = entityId,
                PickupItemCount = itemCount
            });
        }

        public Task ChunkData(Dimension dimension, int chunkX, int chunkZ, ChunkColumnCompactStorage chunkColumn)
        {
            return SendPacket(new ChunkData
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

        public static byte ToByte(GameMode gameMode)
        {
            return (byte)(((uint)gameMode.ModeClass) | (gameMode.IsHardcore ? 0b100u : 0u));
        }

        public Task UnloadChunk(int chunkX, int chunkZ)
        {
            return SendPacket(new UnloadChunk
            {
                ChunkX = chunkX,
                ChunkZ = chunkZ
            });
        }

        public Task SendClientAnimation(uint entityID, ClientboundAnimationId animationID)
        {
            return SendPacket(new ClientboundAnimation
            {
                EntityID = entityID,
                AnimationID = animationID
            });
        }

        public Task BlockChange(Position location, BlockState blockState)
        {
            return SendPacket(new BlockChange
            {
                Location = location,
                BlockId = blockState.ToUInt32()
            });
        }

        public Task ConfirmTransaction(byte windowId, short actionNumber, bool accepted)
        {
            return SendPacket(new ClientboundConfirmTransaction
            {
                WindowId = windowId,
                ActionNumber = actionNumber,
                Accepted = accepted
            });
        }

        public Task OpenWindow(byte windowId, string windowType, Chat windowTitle, byte numberOfSlots, byte? entityId)
        {
            return SendPacket(new OpenWindow
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
            return SendPacket(new ClientboundCloseWindow
            {
                WindowId = windowId
            });
        }

        public Task WindowProperty<T>(byte windowId, T property, short value)
            where T : struct
        {
            short PropertyToShort()
            {
                if (typeof(T) == typeof(FurnaceWindowPropertyType))
                    return Unsafe.As<T, short>(ref property);
                throw new NotSupportedException();
            }

            return SendPacket(new WindowProperty
            {
                WindowId = windowId,
                Property = PropertyToShort(),
                Value = value
            });
        }

        public Task SendPacket(uint packetId, byte[] data)
        {
            if (Sink != null)
                return Sink.SendPacket(packetId, data.AsImmutable());
            else
                return BroadcastSink.SendPacket(packetId, data.AsImmutable(), _except);
        }

        public Task SendPacket(ISerializablePacket packet)
        {
            if (Sink != null)
                return Sink.SendPacket(packet);
            else
                return BroadcastSink.SendPacket(packet, _except);
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
