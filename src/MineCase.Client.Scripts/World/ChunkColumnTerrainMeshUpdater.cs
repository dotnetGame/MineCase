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

        public async void LoadFromChunkData(int chunkX, int chunkZ, ChunkColumnCompactStorage column, NeighborColumns neighborColumns)
        {
            await Task.WhenAll(from i in Enumerable.Range(0, 16)
                               let src = column.Sections[i]
                               where src != null
                               select Sections[i].LoadFromSectionData(src, FindNeighborSections(column, neighborColumns, i)));
        }

        private NeighborSections FindNeighborSections(ChunkColumnCompactStorage column, NeighborColumns neighborColumns, int y)
        {
            ChunkSectionCompactStorage TryGetSection(int sectionY)
            {
                if (sectionY < 0 || sectionY >= ChunkConstants.SectionsPerChunk) return null;
                return column.Sections[sectionY];
            }

            return new NeighborSections
            {
                Left = neighborColumns.Left?.Sections[y],
                Right = neighborColumns.Right?.Sections[y],
                Bottom = TryGetSection(y - 1),
                Top = TryGetSection(y + 1),
                Front = neighborColumns.Front?.Sections[y],
                Back = neighborColumns.Back?.Sections[y],
            };
        }
    }
}
