using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(DungeonLayout dungeonLayout, Mesh result);
    }
}
