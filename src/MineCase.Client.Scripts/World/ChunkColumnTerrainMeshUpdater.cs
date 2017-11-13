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
    public class ChunkColumnTerrainMeshUpdater : MonoBehaviour
    {
        private Mesh _columnMesh;
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
        }

        public async void LoadFromChunkData(int chunkX, int chunkZ, ChunkColumnCompactStorage column)
        {
            _columnMesh = await BuildMesh(column);
            _meshFilter.mesh = _columnMesh;
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }

        private async Task<Mesh> BuildMesh(ChunkColumnCompactStorage column)
        {
            var mesh = new Mesh { name = "Chunk Column" };
            var result = await Task.Run(() =>
            {
                // 计算面总数
                int planeCount = 0;
                for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
                {
                    for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection * ChunkConstants.SectionsPerChunk; y++)
                    {
                        for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                            planeCount += BlockHandler.CalculatePlanesCount(new Vector3Int(x, y, z), column);
                    }
                }

                Debug.Log("Plane Count: " + planeCount);

                // 填充 Mesh
                var vertices = new Vector3[planeCount * 4];
                var normals = new Vector3[planeCount * 4];
                var uvs = new Vector2[planeCount * 4];
                var triangles = new int[planeCount * 6];
                int planeIndex = 0;
                for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
                {
                    for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection * ChunkConstants.SectionsPerChunk; y++)
                    {
                        for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                        {
                            BlockHandler.CreateMesh(vertices, normals, uvs, triangles, ref planeIndex, new Vector3Int(x, y, z), column);
                        }
                    }
                }

                return (vertices, normals, uvs, triangles);
            });

            mesh.vertices = result.vertices;
            mesh.normals = result.normals;
            mesh.uv = result.uvs;
            mesh.triangles = result.triangles;
            return mesh;
        }
    }
}
