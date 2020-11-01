using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor
{
    public class DungeonLayoutEditorCameraView : MonoBehaviour, ILevelEditorCameraView
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
