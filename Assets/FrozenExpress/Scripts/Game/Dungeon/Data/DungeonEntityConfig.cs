using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Data
{
    [System.Serializable]
    public class DungeonEntityConfig
    {
        public string Id => _id;
        public Mesh Mesh => _mesh;
        public Color32 DebugVertexColor => _debugVertexColor;

        public override string ToString()
        {
            return Id;
        }

        [SerializeField] private string _id;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Color32 _debugVertexColor;
    }
}
