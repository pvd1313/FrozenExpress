using FrozenExpress.Scripts.Game.Dungeon.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Controllers
{
    public class DungeonTransform : IDungeonTransform
    {
        private readonly Vector3 _cellSize;

        public DungeonTransform(Vector3 cellSize)
        {
            _cellSize = cellSize;
        }

        public Vector3 TransformPosition(Vector3Int position)
        {
            return Vector3.Scale(position, _cellSize);
        }
    }
}
