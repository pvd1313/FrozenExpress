using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public struct MeshTransform
    {
        public Mesh Mesh;
        public Vector3Int Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public Color32 VertexColor;
    }
}
