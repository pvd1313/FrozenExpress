using UnityEngine;

namespace FrozenExpress.Game.LevelEditor
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
