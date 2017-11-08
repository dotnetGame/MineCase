using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.World
{
    [RequireComponent(typeof(MeshFilter))]
    public class ChunkColumnTerrainMeshUpdater : MonoBehaviour
    {
        private Mesh _chunkMesh;

        private void Start()
        {
            _chunkMesh = new Mesh { name = "Chunk Column" };
            GetComponent<MeshFilter>().mesh = _chunkMesh;
        }

        public void LoadFromChunkData(MineCase.World.ChunkColumnStorage storage)
        {
        }
    }
}
