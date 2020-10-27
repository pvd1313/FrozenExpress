using FrozenExpress.Scripts.Game.Debugging.Enums;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Data
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