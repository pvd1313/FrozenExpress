using FrozenExpress.Scripts.Game.Debugging.Enums;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Data
{
    [System.Serializable]
    public class DebugShaderInstance
    {
        public EDebugShader Shader => _shader;
        public Shader Instance => _instance;
        
        [SerializeField] private EDebugShader _shader;
        [SerializeField] private Shader _instance;
    }
}