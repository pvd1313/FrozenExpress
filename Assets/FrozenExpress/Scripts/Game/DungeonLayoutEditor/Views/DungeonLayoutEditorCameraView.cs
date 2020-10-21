using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public class DungeonLayoutEditorCameraView : MonoBehaviour, IDungeonLayoutEditorCameraView
    {
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}
