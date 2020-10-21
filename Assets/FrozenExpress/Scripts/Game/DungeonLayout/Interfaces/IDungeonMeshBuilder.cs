using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(DungeonLayoutData dungeonLayout, Mesh result);
    }
}
