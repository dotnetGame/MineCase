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

        [JsonProperty(PropertyName = "max-tick-time")]
        [DefaultValue(60000)]
        public uint MaxTickTime { get; set; }

        [JsonProperty(PropertyName = "online-mode")]
        [DefaultValue(false)]
        public bool OnlineMode { get; set; }

        [JsonProperty(PropertyName = "enable-rcon")]
        [DefaultValue(false)]
        public bool EnableRcon { get; set; } // Enables remote access to the server console.

        /// <summary>
        /// Gets or sets by default it allows packets that are n-1 bytes big to go normally, but a packet that n bytes or more will be compressed down. So, lower number means more compression but compressing small amounts of bytes might actually end up with a larger result than what went in.
        /// -1 - disable compression entirely
        /// 0 - compress everything.
        /// </summary>
        [JsonProperty(PropertyName = "network-compression-threshold")]
        [DefaultValue(256)]
        public uint NetworkCompressionThreshold { get; set; }

        [JsonProperty(PropertyName = "resource-pack")]
        [DefaultValue("")]
        public string ResourcePack { get; set; }

        [JsonProperty(PropertyName = "resource-pack-hash")]
        [DefaultValue("")]
        public string ResourcePackHash { get; set; }

        [JsonProperty(PropertyName = "snooper-enabled")]
        [DefaultValue(true)]
        public bool SnooperEnabled { get; set; }

        [JsonProperty(PropertyName = "enable-query")]
        [DefaultValue(false)]
        public bool EnableQuery { get; set; }

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

        [JsonProperty(PropertyName = "generate-settings")]
        [DefaultValue("")]
        public string GenerateSettings { get; set; }

        [JsonProperty(PropertyName = "max-build-height")]
        [DefaultValue(256)]
        public uint MaxBuildHeight { get; set; }

        [JsonProperty(PropertyName = "level-seed")]
        [DefaultValue("")]
        public string LevelSeed { get; set; } // seed for you world

        [JsonProperty(PropertyName = "level-type")]
        [DefaultValue("DEFAULT")]
        public string LevelType { get; set; } // type of you world

        [JsonProperty(PropertyName = "enable-command-block")]
        [DefaultValue(false)]
        public bool EnableCommandBlock { get; set; } // type of you world

        [JsonProperty(PropertyName = "hardcore")]
        [DefaultValue(false)]
        public bool Hardcore { get; set; } // If set to true, players will be set to spectator mode if they die.

        // user
        [JsonProperty(PropertyName = "max-players")]
        [DefaultValue(100)]
        public uint MaxPlayers { get; set; }

        [JsonProperty(PropertyName = "view-distance")]
        [DefaultValue(10)]
        public uint ViewDistance { get; set; }

        [JsonProperty(PropertyName = "allow-flight")]
        [DefaultValue(false)]
        public bool AllowFlight { get; set; }

        [JsonProperty(PropertyName = "player-idle-timeout")]
        [DefaultValue(0)]
        public uint PlayerIdleTimeout { get; set; } // If non-zero, players are kicked from the server if they are idle for more than that many minutes.

        [JsonProperty(PropertyName = "difficulty")]
        [DefaultValue(1)]
        public uint Difficulty { get; set; }

        [JsonProperty(PropertyName = "force-gamemode")]
        [DefaultValue(false)]
        public bool ForceGamemode { get; set; }

        [JsonProperty(PropertyName = "gamemode")]
        [DefaultValue(0)]
        public uint Gamemode { get; set; }

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

        [JsonProperty(PropertyName = "op-permission-level")]
        [DefaultValue(4)]
        public uint OpPermissionLevel { get; set; }
    }
}
