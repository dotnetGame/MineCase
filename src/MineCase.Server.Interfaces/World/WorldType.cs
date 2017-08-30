using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public class WorldType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }

        public bool CanBeCreated { get; set; }

        public bool Versioned { get; set; }

        public string NoticeInfo { get; set; }
    }
}
