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
    [RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
    public class ChunkSectionTerrainMeshUpdater : SmartBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        private BuildResult _buildResult;
        private UserChunkLoaderComponent _chunkLoader;

        protected override void Awake()
        {
            base.Awake();
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            _chunkLoader = FindObjectOfType<UserChunkLoaderComponent>();
        }

        public void LoadFromSectionData(ChunkSectionCompactStorage section, NeighborSections neighbor)
        {
            _buildResult = BuildMesh(section, neighbor);
            _chunkLoader.QueueSectionUpdate(this);
        }

        private BuildResult BuildMesh(ChunkSectionCompactStorage section, NeighborSections neighbor)
        {
            // 计算面总数
            int planeCount = 0;
            for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
            {
                for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection; y++)
                {
                    for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                    {
                        var blockHandler = BlockHandler.Create((BlockId)section.Data[x, y, z].Id, ServiceProvider);
                        planeCount += blockHandler.CalculatePlanesCount(new Vector3Int(x, y, z), section, neighbor);
                    }
                }
            }

            if (planeCount == 0) return null;

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
                        var blockHandler = BlockHandler.Create((BlockId)section.Data[x, y, z].Id, ServiceProvider);
                        blockHandler.CreateMesh(vertices, normals, uvs, triangles, ref planeIndex, new Vector3Int(x, y, z), section, neighbor);
                    }
                }
            }

            return new BuildResult
            {
                Vertices = vertices,
                Normals = normals,
                UVs = uvs,
                Triangles = triangles
            };
        }

        public void InstallMesh()
        {
            Mesh mesh;
            if (_buildResult is BuildResult buildResult)
            {
                mesh = new Mesh { name = "Chunk Section" };

                mesh.vertices = buildResult.Vertices;
                mesh.normals = buildResult.Normals;
                mesh.uv = buildResult.UVs;
                mesh.triangles = buildResult.Triangles;
            }
            else
            {
                mesh = null;
            }

            _meshFilter.mesh = mesh;
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }

        private class BuildResult
        {
            public Vector3[] Vertices;

            public Vector3[] Normals;

            public Vector2[] UVs;

            public int[] Triangles;
        }
    }
}
