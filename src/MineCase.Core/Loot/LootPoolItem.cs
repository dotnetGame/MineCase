using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Loot
{
    public class LootPoolItem
    {
        public int Rolls { get; set; }

        public List<LootEntry> Entries { get; set; }

        public List<LootCondition> Conditions { get; set; }
    }
}
