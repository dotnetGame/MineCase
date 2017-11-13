using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Game.Blocks;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.World
{
    public class ChunkColumnTerrainMeshUpdater : MonoBehaviour
    {
        public ChunkSectionTerrainMeshUpdater[] Sections;

        private void Start()
        {
            if (Sections.Length != 16)
                Debug.LogError("Count of sections must be 16.");
        }

        public async void LoadFromChunkData(int chunkX, int chunkZ, ChunkColumnCompactStorage column)
        {
            await Task.WhenAll(from i in Enumerable.Range(0, 16)
                               let src = column.Sections[i]
                               where src != null
                               select Sections[i].LoadFromSectionData(src));
        }
    }
}
