using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Views
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
