using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    [System.Serializable]
    public class DungeonEditorDrawingConfig
    {
        public Material DiffuseMaterial => _diffuseMaterial;
        public Material DebugMaterial => _debugMaterial;
        public bool DrawDiffuse => _drawDiffuse;

        [SerializeField] private Material _diffuseMaterial;
        [SerializeField] private Material _debugMaterial;
        [SerializeField] private bool _drawDiffuse;
    }
}
