using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    [System.Serializable]
    public class DungeonLayoutEditorCameraControllerConfig
    {
        public float Sensitivity => _sensitivity;

        [SerializeField] private float _sensitivity;
    }
}
