using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Loot
{
    public class LootItem
    {
        public string LootType { get; set; }

        public LootPool Pools { get; set; }
    }
}
