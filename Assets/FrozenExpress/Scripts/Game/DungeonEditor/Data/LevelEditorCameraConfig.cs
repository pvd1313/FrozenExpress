using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor
{
    [System.Serializable]
    public class LevelEditorCameraConfig
    {
        public float Sensitivity => _sensitivity;

        [SerializeField] private float _sensitivity;
    }
}
