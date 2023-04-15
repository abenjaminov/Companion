using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.TerrainGeneration
{
    public struct GenerateTerrainMeshDataOptions
    {
        public float[,] HeightMap;
        public float HeightMultiplier;
        public AnimationCurve MeshHeightCurve;
        public int LevelOfDetail;
    }
    public static class MeshGenerator
    {
        public static MeshData GenerateTerrainMeshData(GenerateTerrainMeshDataOptions options)
        {
            var heightMultiplierCurve = new AnimationCurve(options.MeshHeightCurve.keys);
            var heightMap = options.HeightMap;
            
            var width = heightMap.GetLength(0);
            var height = heightMap.GetLength(1);

            var topLeftX = (width - 1) / -2f;
            var topLeftZ = (height - 1) / 2f;

            var levelOfDetails = options.LevelOfDetail == 0 ? 1 : options.LevelOfDetail;
            
            var meshSimplificationIncrement = levelOfDetails * 2;
            var verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;
            
            var meshData = new MeshData(verticesPerLine, verticesPerLine);
            var vertexIndex = 0;
            
            for (int y = 0; y < height; y += meshSimplificationIncrement)
            {
                for (int x = 0; x < width; x += meshSimplificationIncrement)
                {
                    var meshHeight = heightMultiplierCurve.Evaluate(heightMap[x, y]) * options.HeightMultiplier;
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, meshHeight, topLeftZ - y);
                    meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                    
                    if (x < width - 1 && y < height - 1)
                    {
                        meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                        meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                    }
                    
                    vertexIndex++;
                }
            }

            return meshData;
        }
    }

    public class MeshData
    {
        public Vector3[] vertices;
        public List<int> triangles;
        public Vector2[] uvs;

        public MeshData(int meshWidth, int meshHeight)
        {
            vertices = new Vector3[meshHeight * meshWidth];
            uvs = new Vector2[meshWidth * meshHeight];
            triangles = new List<int>((meshHeight - 1) * (meshWidth - 1) * 6);
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles.Add(a);
            triangles.Add(b);
            triangles.Add(c);
        }
        
        public Mesh CreateMesh()
        {
            var mesh = new Mesh()
            {
                vertices = vertices,
                triangles = triangles.ToArray(),
                uv = uvs
            };
            
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}