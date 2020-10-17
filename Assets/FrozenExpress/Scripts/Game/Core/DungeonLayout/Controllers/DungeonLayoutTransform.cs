using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutTransform : IDungeonLayoutTransform
    {
        private readonly Vector3 _cellSize;

        public DungeonLayoutTransform(Vector3 cellSize)
        {
            _cellSize = cellSize;
        }

        public Vector3 TransformPosition(Vector3Int position)
        {
            return Vector3.Scale(position, _cellSize);
        }
    }
}
