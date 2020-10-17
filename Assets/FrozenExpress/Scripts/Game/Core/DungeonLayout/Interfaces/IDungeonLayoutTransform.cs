using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonLayoutTransform
    {
        Vector3 TransformPosition(Vector3Int position);
    }
}
