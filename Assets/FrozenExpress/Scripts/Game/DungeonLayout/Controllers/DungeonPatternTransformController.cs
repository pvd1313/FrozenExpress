using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonPatternTransformController : IDungeonPatternTransformController
    {
        private readonly Quaternion _angleAxisY90 = Quaternion.AngleAxis(90, Vector3.up);
        private readonly Quaternion _angleAxisY180 = Quaternion.AngleAxis(180, Vector3.up);
        private readonly Quaternion _angleAxisY270 = Quaternion.AngleAxis(270, Vector3.up);

        public Vector3Int TransformDirection(Vector3Int direction, EPatternMatrixTransform transform)
        {
            int temp;

            switch (transform)
            {
                case EPatternMatrixTransform.MirrorX:
                    direction.x = -direction.x;
                    break;

                case EPatternMatrixTransform.MirrorY:
                    direction.y = -direction.y;
                    break;

                case EPatternMatrixTransform.MirrorZ:
                    direction.z = -direction.z;
                    break;

                case EPatternMatrixTransform.RotateY90:
                    temp = direction.x;
                    direction.x = direction.z;
                    direction.z = -temp;
                    break;

                case EPatternMatrixTransform.RotateY180:
                    direction.x = -direction.x;
                    direction.z = -direction.z;
                    break;

                case EPatternMatrixTransform.RotateY270:
                    temp = direction.x;
                    direction.x = -direction.z;
                    direction.z = temp;
                    break;
            }
            
            return direction;
        }

        public Quaternion TransformRotation(Quaternion rotation, EPatternMatrixTransform transform)
        {
            switch (transform)
            {
                case EPatternMatrixTransform.RotateY90:
                    rotation *= _angleAxisY90;
                    break;

                case EPatternMatrixTransform.RotateY180:
                    rotation *= _angleAxisY180;
                    break;

                case EPatternMatrixTransform.RotateY270:
                    rotation *= _angleAxisY270;
                    break;
            }

            return rotation;
        }

        public Vector3 TransformScale(Vector3 scale, EPatternMatrixTransform transform)
        {
            switch (transform)
            {
                case EPatternMatrixTransform.MirrorX:
                    scale.x = -scale.x;
                    break;

                case EPatternMatrixTransform.MirrorY:
                    scale.y = -scale.y;
                    break;

                case EPatternMatrixTransform.MirrorZ:
                    scale.z = -scale.z;
                    break;
            }

            return scale;
        }
    }
}
