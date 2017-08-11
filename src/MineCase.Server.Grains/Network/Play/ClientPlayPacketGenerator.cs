﻿using MineCase.Protocol.Play;
using MineCase.Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Play
{
    class ClientPlayPacketGenerator
    {
        public IClientboundPacketSink Sink { get; }

        public ClientPlayPacketGenerator(IClientboundPacketSink sink)
        {
            Sink = sink;
        }

        public Task JoinGame(uint eid, GameMode gameMode, Dimension dimension, Difficulty difficulty, byte maxPlayers, string levelType, bool reducedDebugInfo)
        {
            return Sink.SendPacket(new JoinGame
            {
                EID = (int)eid,
                GameMode = (byte)(((uint)gameMode.ModeClass) | (gameMode.IsHardcore ? 0b100u : 0u)),
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

        public Task WindowItems(byte windowId, IReadOnlyList<Game.Slot> slots)
        {
            return Sink.SendPacket(new WindowItems
            {
                WindowId = windowId,
                Count = (short)slots.Count,
                Slots = slots.Select(o => TransformSlotData(o)).ToArray()
            });
        }

        private Protocol.Play.Slot TransformSlotData(Game.Slot o)
        {
            return new Protocol.Play.Slot
            {
                BlockId = -1
            };
        }
    }
}
