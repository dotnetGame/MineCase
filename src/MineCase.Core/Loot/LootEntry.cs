using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Loot
{
    public class LootEntry
    {
        public string Type { get; set; }

        public List<LootCondition> Conditions { get; set; }

        public string Name { get; set; }
    }
}
