using UnityEngine;

namespace FrozenExpress.LevelEditor
{
    [System.Serializable]
    public class LevelEditorCameraConfig
    {
        public float Sensitivity => _sensitivity;

        [SerializeField] private float _sensitivity;
    }
}
