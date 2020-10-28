using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonPatternTransformController : IDungeonPatternTransformController
    {
        private readonly Quaternion _angleAxisY90 = Quaternion.AngleAxis(90, Vector3.up);
        private readonly Quaternion _angleAxisY180 = Quaternion.AngleAxis(180, Vector3.up);
        private readonly Quaternion _angleAxisY270 = Quaternion.AngleAxis(270, Vector3.up);

        public Vector3Int TransformDirection(Vector3Int direction, EPatternTransform transform)
        {
            int temp;

            switch (transform)
            {
                case EPatternTransform.MirrorX:
                    direction.x = -direction.x;
                    break;

                case EPatternTransform.MirrorY:
                    direction.y = -direction.y;
                    break;

                case EPatternTransform.MirrorZ:
                    direction.z = -direction.z;
                    break;

                case EPatternTransform.RotateY90:
                    temp = direction.x;
                    direction.x = direction.z;
                    direction.z = -temp;
                    break;

                case EPatternTransform.RotateY180:
                    direction.x = -direction.x;
                    direction.z = -direction.z;
                    break;

                case EPatternTransform.RotateY270:
                    temp = direction.x;
                    direction.x = -direction.z;
                    direction.z = temp;
                    break;
            }
            
            return direction;
        }

        public Quaternion TransformRotation(Quaternion rotation, EPatternTransform transform)
        {
            switch (transform)
            {
                case EPatternTransform.RotateY90:
                    rotation *= _angleAxisY90;
                    break;

                case EPatternTransform.RotateY180:
                    rotation *= _angleAxisY180;
                    break;

                case EPatternTransform.RotateY270:
                    rotation *= _angleAxisY270;
                    break;
            }

            return rotation;
        }

        public Vector3 TransformScale(Vector3 scale, EPatternTransform transform)
        {
            switch (transform)
            {
                case EPatternTransform.MirrorX:
                    scale.x = -scale.x;
                    break;

                case EPatternTransform.MirrorY:
                    scale.y = -scale.y;
                    break;

                case EPatternTransform.MirrorZ:
                    scale.z = -scale.z;
                    break;
            }

            return scale;
        }
    }
}
