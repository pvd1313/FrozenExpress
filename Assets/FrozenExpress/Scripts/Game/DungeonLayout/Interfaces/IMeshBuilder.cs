using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IMeshBuilder
    {
        void AddMesh(Mesh mesh, Matrix4x4 matrix, Color32 color);

        void Apply(Mesh target);

        void Clear();
    }
}
