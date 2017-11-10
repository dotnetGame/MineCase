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
        public MeshFilter CubeMesh;
        public Material CubeMaterial;

        private Mesh _cubeMesh;
        private List<Matrix4x4>[] _blockPositions;

        private void Start()
        {
            _cubeMesh = CubeMesh.sharedMesh;
        }

        public void LoadFromChunkData(int chunkX, int chunkZ, ChunkColumnCompactStorage column)
        {
            transform.position = new Vector3(chunkX * 16, 0, chunkZ * 16);

            _blockPositions = new List<Matrix4x4>[ChunkConstants.BlockEdgeWidthInSection];
            for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
            {
                var positions = new List<Matrix4x4>(ChunkConstants.BlockEdgeWidthInSection * ChunkConstants.BlockEdgeWidthInSection);
                for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection; y++)
                {
                    for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                    {
                        var offset = new Vector3(x, y, z);
                        var matrix = Matrix4x4.Translate(offset) * transform.localToWorldMatrix;
                        var state = column[x, y, z];
                        if (state.Id != (uint)BlockId.Air)
                            positions.Add(matrix);
                    }
                }

                _blockPositions[z] = positions;
            }
        }

        private void OnRenderObject()
        {
            if (_blockPositions != null)
            {
                foreach (var positions in _blockPositions)
                {
                    if (positions.Count != 0)
                        UnityEngine.Graphics.DrawMeshInstanced(_cubeMesh, 0, CubeMaterial, positions);
                }
            }
        }
    }
}
