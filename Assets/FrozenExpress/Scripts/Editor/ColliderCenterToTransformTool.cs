using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor
{
    public static class ColliderCenterToTransformTool
    {
        [MenuItem("CONTEXT/BoxCollider/Copy center to transform")]
        private static void BoxCenterToTransform(MenuCommand command)
        {
            var target = command.context as BoxCollider;
            target.transform.position += target.center;
            target.center = Vector3.zero;
        }
    }
}

