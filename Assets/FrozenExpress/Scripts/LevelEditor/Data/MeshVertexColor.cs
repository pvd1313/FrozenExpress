using UnityEngine;

namespace FrozenExpress.LevelEditor
{
    [System.Serializable]
    public class MeshVertexColor
    {
        public Color32 Color => _color;
        public Mesh Mesh => _mesh;

        [SerializeField] private Color32 _color;
        [SerializeField] private Mesh _mesh;
    }
}
