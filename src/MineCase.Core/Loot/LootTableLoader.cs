using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MineCase.Loot
{
    public class LootTableLoader
    {
        public List<LootItem> LootItems { get; } = new List<LootItem>();

        public async Task LoadLootTable(StreamReader streamReader)
        {
            LootItem item = JsonConvert.DeserializeObject<LootItem>(await streamReader.ReadToEndAsync());
            LootItems.Add(item);
        }
    }
}
