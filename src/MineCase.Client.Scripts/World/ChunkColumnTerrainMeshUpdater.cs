using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Game.Blocks;
using MineCase.Engine;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.World
{
    public class ChunkColumnTerrainMeshUpdater : MonoBehaviour
    {
        public GameObject Section;

        private readonly ChunkSectionTerrainMeshUpdater[] _sections = new ChunkSectionTerrainMeshUpdater[16];

        public void LoadFromChunkData(int chunkX, int chunkZ, ChunkColumnCompactStorage column, NeighborColumns neighborColumns)
        {
            for (int i = 0; i < 16; i++)
            {
                var src = column.Sections[i];
                if (src != null)
                    InstantiateSection(i, src, column, neighborColumns);
            }
        }

        private ChunkSectionTerrainMeshUpdater InstantiateSection(int y, ChunkSectionCompactStorage src, ChunkColumnCompactStorage column, NeighborColumns neighborColumns)
        {
            var updater = _sections[y];
            if (!updater)
            {
                Execute.OnMainThread(() =>
                {
                    var go = Instantiate(Section, transform);
                    go.name = "Section " + y;
                    go.transform.localPosition = new Vector3(0, y * 16, 0);
                    updater = go.GetComponent<ChunkSectionTerrainMeshUpdater>();
                });
                _sections[y] = updater;
            }

            updater.LoadFromSectionData(src, FindNeighborSections(column, neighborColumns, y));
            return updater;
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
