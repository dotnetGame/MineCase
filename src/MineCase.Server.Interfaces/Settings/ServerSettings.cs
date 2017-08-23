using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Settings
{
    public class ServerSettings
    {
        // server
        public string Motd { get; set; } // this is the message that is displayed in the server list of the client

        public string ServerIp { get; set; }

        public int ServerPort { get; set; }

        // spawn
        public int SpawnProtection { get; set; }

        public bool SpawnMonsters { get; set; }

        public bool SpawnNpcs { get; set; }

        public bool SpawnAnimals { get; set; }

        // world
        public int MaxWorldSize { get; set; }

        public bool AllowNether { get; set; }

        public bool GenerateStructures { get; set; }

        public int MaxBuildHeight { get; set; }

        public string LevelSeed { get; set; } // seed for you world

        // user
        public uint MaxPlayers { get; set; }

        public int ViewDistance { get; set; }

        public int PlayerIdleTimeout { get; set; } // If non-zero, players are kicked from the server if they are idle for more than that many minutes.

        public int Gamemode { get; set; }

        public int Difficulty { get; set; }

        public bool Pvp { get; set; }

        public bool AnnouncePlayerAchievements { get; set; }

        // op
        public bool WhiteList { get; set; }

        // other
    }
}
