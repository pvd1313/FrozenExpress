using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Data
{
    [System.Serializable]
    public class LevelEditorCameraConfig
    {
        public float Sensitivity => _sensitivity;

        [SerializeField] private float _sensitivity;
    }
}
