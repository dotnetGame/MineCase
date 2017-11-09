using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    public partial class BlockHandler
    {
        private static readonly Vector3[,] _positions = new Vector3[6, 4]
        {
            { new Vector3(-1, -1, -1), new Vector3(-1, -1, +1), new Vector3(-1, +1, -1), new Vector3(-1, +1, +1) },
            { new Vector3(+1, -1, -1), new Vector3(+1, -1, +1), new Vector3(+1, +1, -1), new Vector3(+1, +1, +1) },
            { new Vector3(-1, +1, -1), new Vector3(-1, +1, +1), new Vector3(+1, +1, -1), new Vector3(+1, +1, +1) },
            { new Vector3(-1, -1, -1), new Vector3(-1, -1, +1), new Vector3(+1, -1, -1), new Vector3(+1, -1, +1) },
            { new Vector3(-1, -1, -1), new Vector3(-1, +1, -1), new Vector3(+1, -1, -1), new Vector3(+1, +1, -1) },
            { new Vector3(-1, -1, +1), new Vector3(-1, +1, +1), new Vector3(+1, -1, +1), new Vector3(+1, +1, +1) }
        };

        private static readonly Vector3[] _normals = new Vector3[6]
        {
            new Vector3(-1, 0, 0),
            new Vector3(+1, 0, 0),
            new Vector3(0, +1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, +1)
        };

        private static readonly Vector2[,] _uvs = new Vector2[6, 4]
        {
            { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) },
            { new Vector2(1, 0), new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1) },
            { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0) },
            { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) },
            { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) },
            { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 0), new Vector2(0, 1) }
        };

        private static readonly int[,] _indices = new int[6, 6]
        {
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 },
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 },
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 }
        };

        public void CreateMesh(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int[] triangles, ref int planeIndex, Vector3 offset)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int n = planeIndex * 4, k = 0; k < 4; k++, n++)
                {
                    var v = _positions[i, k];
                    v += offset;
                    vertices[n] = v;
                    normals[n] = _normals[i];
                    uvs[n] = _uvs[i, k];
                }

                for (int n = planeIndex * 6, k = 0; k < 6; k++, n++)
                    triangles[n] = planeIndex * 4 + _indices[i, k];

                planeIndex++;
            }
        }
    }
}
