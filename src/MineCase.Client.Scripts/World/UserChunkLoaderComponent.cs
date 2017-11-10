using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.World
{
    public class UserChunkLoaderComponent : SmartBehaviour
    {
        public GameObject ChunkColumn;

        public void LoadTerrain(int chunkX, int chunkZ, ChunkColumnCompactStorage column)
        {
            Debug.Log($"Load Chunk: {chunkX}, {chunkZ}");
            var go = Instantiate(ChunkColumn);
            go.GetComponentInChildren<ChunkColumnTerrainMeshUpdater>().LoadFromChunkData(chunkX, chunkZ, column);
        }
    }
}
