using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly List<Color32> _colors;
        private readonly List<Vector3> _vertices;
        private readonly List<int> _triangles;
        private readonly List<Vector3> _normals;
        private readonly List<Vector4> _tangents;

        private readonly List<Vector3> _vertexBuffer;
        private readonly List<int> _triangleBuffer;
        private readonly List<Vector3> _normalBuffer;
        private readonly List<Vector4> _tangentBuffer;

        public MeshBuilder()
        {
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _colors = new List<Color32>();
            _normals = new List<Vector3>();
            _tangents = new List<Vector4>();

            _vertexBuffer = new List<Vector3>();
            _triangleBuffer = new List<int>();
            _normalBuffer = new List<Vector3>();
            _tangentBuffer = new List<Vector4>();
        }

        public void AddMesh(Mesh mesh, Matrix4x4 matrix, Color32 color)
        {
            mesh.GetVertices(_vertexBuffer);

            var vertexFirst = _vertices.Count;
            var vertexCount = _vertexBuffer.Count;

            for (var i = 0; i < vertexCount; i++)
            {
                var point = matrix.MultiplyPoint(_vertexBuffer[i]);
                _vertices.Add(point);
                _colors.Add(color);
            }

            mesh.GetTriangles(_triangleBuffer, 0);

            var det = matrix.determinant;

            if (det > 0)
            {
                for (var i = 0; i < _triangleBuffer.Count; i++)
                {
                    _triangles.Add(vertexFirst + _triangleBuffer[i]);
                }
            }
            else
            {
                for (var i = _triangleBuffer.Count - 1; i >= 0; i--)
                {
                    _triangles.Add(vertexFirst + _triangleBuffer[i]);
                }
            }

            mesh.GetNormals(_normalBuffer);
            for (var i = 0; i < _normalBuffer.Count; i++)
            {
                var normal = matrix.MultiplyVector(_normalBuffer[i]);
                _normals.Add(normal);
            }

            mesh.GetTangents(_tangentBuffer);
            for (var i = 0; i < _tangentBuffer.Count; i++)
            {
                var tangent = matrix.MultiplyVector(_tangentBuffer[i]);
                _tangents.Add(tangent);
            }
        }

        public void Apply(Mesh target)
        {
            target.Clear();
            target.SetVertices(_vertices);
            target.SetTriangles(_triangles, 0);
            target.SetColors(_colors);
            target.SetNormals(_normals);
            target.SetTangents(_tangents);

            target.RecalculateBounds();
            target.Optimize();
        }

        public void Clear()
        {
            _vertices.Clear();
            _triangles.Clear();
            _colors.Clear();
            _normals.Clear();
            _tangents.Clear();
        }
    }
}
