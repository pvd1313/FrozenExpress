using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonPatternTransformController
    {
        Vector3 TransformScale(Vector3 scale, EPatternMatrixTransform transform);

        Quaternion TransformRotation(Quaternion rotation, EPatternMatrixTransform transform);

        Vector3Int TransformDirection(Vector3Int direction, EPatternMatrixTransform transform);
    }
}
