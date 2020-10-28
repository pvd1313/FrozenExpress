using UnityEngine;

namespace FrozenExpress.Game.Debugging
{
    [System.Serializable]
    public class DebugShapeMesh
    {
        public EDebugShape Shape => _shape;
        public Mesh Mesh => _mesh;

        [SerializeField] private EDebugShape _shape;
        [SerializeField] private Mesh _mesh;
    }
}