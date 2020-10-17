using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonLayoutMeshBuilder
    {
        void BuildMesh(DungeonLayoutData dungeonLayout, Mesh result);
    }
}
