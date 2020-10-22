using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonTransform
    {
        Vector3 TransformPosition(Vector3Int position);
    }
}
