using UnityEngine;

namespace FrozenExpress.Game.LevelEditor
{
    [System.Serializable]
    public class LevelEditorCameraConfig
    {
        public float Sensitivity => _sensitivity;

        [SerializeField] private float _sensitivity;
    }
}
