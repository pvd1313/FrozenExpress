using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonTransform
    {
        Vector3 TransformPosition(Vector3Int position);
    }
}
