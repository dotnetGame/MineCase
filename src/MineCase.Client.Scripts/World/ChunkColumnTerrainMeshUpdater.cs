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
        private List<Matrix4x4> _blockPositions;

        private void Start()
        {
            _cubeMesh = CubeMesh.sharedMesh;
            LoadFromChunkData();
        }

        public void LoadFromChunkData()
        {
            _blockPositions = new List<Matrix4x4>(4 * 4 * 4);
            for (int z = 0; z < 4; z++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        var offset = new Vector3(x, y, z);
                        var matrix = Matrix4x4.Translate(offset) * transform.localToWorldMatrix;
                        _blockPositions.Add(matrix);
                    }
                }
            }
        }

        private void Update()
        {
            UnityEngine.Graphics.DrawMeshInstanced(_cubeMesh, 0, CubeMaterial, _blockPositions);
        }
    }
}
