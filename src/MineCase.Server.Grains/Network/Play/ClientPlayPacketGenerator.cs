﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Protocol.Play;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
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

        public Task WindowItems(byte windowId, IReadOnlyList<Game.Slot> slots)
        {
            return Sink.SendPacket(new WindowItems
            {
                WindowId = windowId,
                Count = (short)slots.Count,
                Slots = slots.Select(o => TransformSlotData(o)).ToArray()
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

        private static Protocol.Play.Slot TransformSlotData(Game.Slot o)
        {
            return new Protocol.Play.Slot
            {
                BlockId = -1
            };
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
