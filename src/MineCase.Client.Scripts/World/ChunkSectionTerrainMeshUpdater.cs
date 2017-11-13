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
    [RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
    public class ChunkSectionTerrainMeshUpdater : MonoBehaviour
    {
        private Mesh _columnMesh;
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
        }

        public async Task LoadFromSectionData(ChunkSectionCompactStorage section)
        {
            _columnMesh = await BuildMesh(section);
            _meshFilter.mesh = _columnMesh;
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }

        private async Task<Mesh> BuildMesh(ChunkSectionCompactStorage section)
        {
            var mesh = new Mesh { name = "Chunk Section" };
            var result = await Task.Run(() =>
            {
                // 计算面总数
                int planeCount = 0;
                for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
                {
                    for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection; y++)
                    {
                        for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                            planeCount += BlockHandler.CalculatePlanesCount(new Vector3Int(x, y, z), section);
                    }
                }

                if (planeCount != 0)
                    Debug.Log("Plane Count: " + planeCount);

                // 填充 Mesh
                var vertices = new Vector3[planeCount * 4];
                var normals = new Vector3[planeCount * 4];
                var uvs = new Vector2[planeCount * 4];
                var triangles = new int[planeCount * 6];
                int planeIndex = 0;
                for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
                {
                    for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection; y++)
                    {
                        for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                        {
                            BlockHandler.CreateMesh(vertices, normals, uvs, triangles, ref planeIndex, new Vector3Int(x, y, z), section);
                        }
                    }
                }

                return (vertices, normals, uvs, triangles);
            });

            if (result.vertices.Length == 0) return null;

            mesh.vertices = result.vertices;
            mesh.normals = result.normals;
            mesh.uv = result.uvs;
            mesh.triangles = result.triangles;
            return mesh;
        }
    }
}
