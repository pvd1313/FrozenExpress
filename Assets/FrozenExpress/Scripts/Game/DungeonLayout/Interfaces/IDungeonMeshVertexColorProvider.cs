using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonMeshVertexColorProvider
    {
        Color32 GetColor(Mesh mesh);
    }
}
