using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.World
{
    public struct NeighborColumns
    {
        public ChunkColumnCompactStorage Left;

        public ChunkColumnCompactStorage Right;

        public ChunkColumnCompactStorage Front;

        public ChunkColumnCompactStorage Back;
    }

    public class UserChunkLoaderComponent : SmartBehaviour
    {
        public GameObject ChunkColumn;

        private Dictionary<Vector2Int, ChunkColumnCompactStorage> _sections;
        private Dictionary<Vector2Int, ChunkColumnTerrainMeshUpdater> _updater;
        private ConcurrentQueue<ChunkSectionTerrainMeshUpdater> _deferredUpdater;

        private void Start()
        {
            _sections = new Dictionary<Vector2Int, ChunkColumnCompactStorage>();
            _updater = new Dictionary<Vector2Int, ChunkColumnTerrainMeshUpdater>();
            _deferredUpdater = new ConcurrentQueue<ChunkSectionTerrainMeshUpdater>();
        }

        private void Update()
        {
            if (_deferredUpdater.TryDequeue(out var updater))
            {
                if (updater)
                    updater.InstallMesh();
            }
        }

        public void LoadTerrain(int chunkX, int chunkZ, ChunkColumnCompactStorage column)
        {
            var key = new Vector2Int(chunkX, chunkZ);
            _sections.Add(key, column);

            var updater = InstantiateUpdater(chunkX, chunkZ);
            _updater.Add(key, updater);
            updater?.LoadFromChunkData(chunkX, chunkZ, column, FindNeighborColumns(key));

            UpdateNeighborChunks(key);
        }

        private ChunkColumnTerrainMeshUpdater InstantiateUpdater(int chunkX, int chunkZ)
        {
            ChunkColumnTerrainMeshUpdater updater = null;
            Execute.OnMainThread(() =>
            {
                var go = Instantiate(ChunkColumn);
                go.name = $"Chunk Column ({chunkX}, {chunkZ})";
                go.transform.position = new Vector3(chunkX * 16, 0, chunkZ * 16);
                updater = go.GetComponentInChildren<ChunkColumnTerrainMeshUpdater>();
            });
            return updater;
        }

        private static readonly Vector2Int[] _neighbors = new[]
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1)
        };

        private NeighborColumns FindNeighborColumns(Vector2Int origin)
        {
            var columns = default(NeighborColumns);
            _sections.TryGetValue(origin + _neighbors[0], out columns.Left);
            _sections.TryGetValue(origin + _neighbors[1], out columns.Right);
            _sections.TryGetValue(origin + _neighbors[2], out columns.Front);
            _sections.TryGetValue(origin + _neighbors[3], out columns.Back);
            return columns;
        }

        private void UpdateNeighborChunks(Vector2Int origin)
        {
            foreach (var offset in _neighbors)
                TryUpdateChunk(origin + offset);
        }

        private void TryUpdateChunk(Vector2Int key)
        {
            if (_updater.TryGetValue(key, out var updater) && _sections.TryGetValue(key, out var column))
            {
                if (updater)
                    updater.LoadFromChunkData(key.x, key.y, column, FindNeighborColumns(key));
            }
        }

        public void QueueSectionUpdate(ChunkSectionTerrainMeshUpdater updater)
        {
            _deferredUpdater.Enqueue(updater);
        }
    }
}
