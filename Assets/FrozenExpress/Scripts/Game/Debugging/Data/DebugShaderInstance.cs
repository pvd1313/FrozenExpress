using UnityEngine;

namespace FrozenExpress.Game.Debugging
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