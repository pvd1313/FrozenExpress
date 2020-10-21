using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(DungeonLayout dungeonLayout, Mesh result);
    }
}
