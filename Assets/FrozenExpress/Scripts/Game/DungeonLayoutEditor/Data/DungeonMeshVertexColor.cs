using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    [System.Serializable]
    public class DungeonMeshVertexColor
    {
        public Color32 Color => _color;
        public Mesh Mesh => _mesh;

        [SerializeField] private Color32 _color;
        [SerializeField] private Mesh _mesh;
    }
}
