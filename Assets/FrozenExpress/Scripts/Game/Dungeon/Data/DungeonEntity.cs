using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class DungeonEntity
    {
        public string Id => _id;
        public Mesh Mesh => _mesh;
        public Color32 DebugVertexColor => _debugVertexColor;
        public IReadOnlyList<ColliderBounds> Colliders => _colliders;

        public override string ToString()
        {
            return Id;
        }

        [SerializeField] private string _id;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Color32 _debugVertexColor;
        [SerializeField] private List<ColliderBounds> _colliders;
    }
}
