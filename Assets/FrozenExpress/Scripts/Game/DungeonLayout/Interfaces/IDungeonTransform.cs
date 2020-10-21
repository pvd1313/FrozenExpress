using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonTransform
    {
        Vector3 TransformPosition(Vector3Int position);
    }
}
