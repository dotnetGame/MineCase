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
            var go = Instantiate(ChunkColumn);
            go.transform.position = new Vector3(chunkX * 16, 0, chunkZ * 16);
            go.GetComponentInChildren<ChunkColumnTerrainMeshUpdater>().LoadFromChunkData(chunkX, chunkZ, column);
        }
    }
}
