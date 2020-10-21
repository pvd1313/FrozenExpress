using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonTransform
    {
        Vector3 TransformPosition(Vector3Int position);
    }
}
