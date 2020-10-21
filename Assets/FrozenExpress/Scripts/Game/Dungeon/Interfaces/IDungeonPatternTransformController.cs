using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonPatternTransformController
    {
        Vector3 TransformScale(Vector3 scale, EPatternTransform transform);

        Quaternion TransformRotation(Quaternion rotation, EPatternTransform transform);

        Vector3Int TransformDirection(Vector3Int direction, EPatternTransform transform);
    }
}
