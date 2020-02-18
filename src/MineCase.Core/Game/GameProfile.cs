using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Game
{
    public class GameProfile
    {
        public string UUID { get; set; } = null;

        public string Name { get; set; } = null;

        public GameProfile()
        {
        }

        public GameProfile(string uuid, string name)
        {
            UUID = uuid;
            Name = name;
        }

        public bool Complete{
            get { return UUID != null && Name != null; }
        }
    }
}
