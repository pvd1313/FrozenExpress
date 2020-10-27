using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Data
{
    [System.Serializable]
    public class DebugDrawingConfig
    {
        public IReadOnlyList<DebugShapeMesh> Meshes => _meshes;
        public IReadOnlyList<DebugShaderInstance> Shaders => _shaders;
        public IReadOnlyList<DebugColorValue> Colors => _colors;

        [OneLine]
        [SerializeField] private List<DebugColorValue> _colors;
        
        [OneLine]
        [SerializeField] private List<DebugShapeMesh> _meshes;
        
        [OneLine]
        [SerializeField] private List<DebugShaderInstance> _shaders;
    }
}