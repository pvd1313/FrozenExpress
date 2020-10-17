using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public struct DungeonLayoutEntityMesh
    {
        public Mesh Mesh { get; set; }
        public Vector3Int Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}
