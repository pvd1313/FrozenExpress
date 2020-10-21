using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public class DungeonLayoutEditorModelView : MonoBehaviour, IDungeonLayoutEditorModelView
    {
        [SerializeField] MeshFilter _meshFilter;

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }
    }
}
