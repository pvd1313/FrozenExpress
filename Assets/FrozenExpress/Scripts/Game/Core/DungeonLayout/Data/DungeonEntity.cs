using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public struct DungeonEntity
    {
        public Mesh Mesh { get; set; }
        public Vector3Int Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}
