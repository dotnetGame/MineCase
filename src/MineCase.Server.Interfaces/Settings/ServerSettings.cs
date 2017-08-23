using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MineCase.Server.Game;
using Newtonsoft.Json;

namespace MineCase.Server.Settings
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ServerSettings
    {
        // server
        [JsonProperty(PropertyName = "motd")]
        [DefaultValue("A Minecraft Server")]
        public string Motd { get; set; } // this is the message that is displayed in the server list of the client

        [JsonProperty(PropertyName = "server-ip")]
        [DefaultValue("127.0.0.1")]
        public string ServerIp { get; set; }

        [JsonProperty(PropertyName = "server-port")]
        [DefaultValue(25565)]
        public uint ServerPort { get; set; }

        // spawn
        [JsonProperty(PropertyName = "spawn-protection")]
        [DefaultValue(16)]
        public uint SpawnProtection { get; set; }

        [JsonProperty(PropertyName = "spawn-monsters")]
        [DefaultValue(true)]
        public bool SpawnMonsters { get; set; }

        [JsonProperty(PropertyName = "spawn-npcs")]
        [DefaultValue(true)]
        public bool SpawnNpcs { get; set; }

        [JsonProperty(PropertyName = "spawn-animals")]
        [DefaultValue(true)]
        public bool SpawnAnimals { get; set; }

        // world
        [JsonProperty(PropertyName = "max-world-size")]
        [DefaultValue(29999984)]
        public uint MaxWorldSize { get; set; }

        [JsonProperty(PropertyName = "allow-nether")]
        [DefaultValue(true)]
        public bool AllowNether { get; set; }

        [JsonProperty(PropertyName = "generate-structures")]
        [DefaultValue(true)]
        public bool GenerateStructures { get; set; }

        [JsonProperty(PropertyName = "max-build-height")]
        [DefaultValue(256)]
        public uint MaxBuildHeight { get; set; }

        [JsonProperty(PropertyName = "level-seed")]
        [DefaultValue("")]
        public string LevelSeed { get; set; } // seed for you world

        [JsonProperty(PropertyName = "level-type")]
        [DefaultValue("DEFAULT")]
        public string LevelType { get; set; } // type of you world

        // user
        [JsonProperty(PropertyName = "max-players")]
        [DefaultValue(100)]
        public uint MaxPlayers { get; set; }

        [JsonProperty(PropertyName = "view-distance")]
        [DefaultValue(10)]
        public uint ViewDistance { get; set; }

        [JsonProperty(PropertyName = "player-idle-timeout")]
        [DefaultValue(0)]
        public uint PlayerIdleTimeout { get; set; } // If non-zero, players are kicked from the server if they are idle for more than that many minutes.

        [JsonProperty(PropertyName = "game-mode")]
        [DefaultValue(GameMode.Class.Survival)]
        public GameMode Gamemode { get; set; }

        [JsonProperty(PropertyName = "difficulty")]
        [DefaultValue(MineCase.Server.Game.Difficulty.Easy)]
        public Difficulty Difficulty { get; set; }

        [JsonProperty(PropertyName = "pvp")]
        [DefaultValue(true)]
        public bool Pvp { get; set; }

        [JsonProperty(PropertyName = "announce-player-achievements")]
        [DefaultValue(true)]
        public bool AnnouncePlayerAchievements { get; set; }

        // op
        [JsonProperty(PropertyName = "white-list")]
        [DefaultValue(false)]
        public bool WhiteList { get; set; }

        // other
    }
}
