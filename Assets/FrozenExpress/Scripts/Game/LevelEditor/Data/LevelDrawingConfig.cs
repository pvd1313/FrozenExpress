using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Data
{
    [System.Serializable]
    public class LevelDrawingConfig
    {
        public Material DiffuseMaterial => _diffuseMaterial;
        public Material DebugMaterial => _debugMaterial;
        public bool DrawDiffuse => _drawDiffuse;

        [SerializeField] private Material _diffuseMaterial;
        [SerializeField] private Material _debugMaterial;
        [SerializeField] private bool _drawDiffuse;
    }
}
