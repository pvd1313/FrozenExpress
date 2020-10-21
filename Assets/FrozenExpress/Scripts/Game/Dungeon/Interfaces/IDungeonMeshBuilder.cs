using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(DungeonLayout dungeonLayout, Mesh result);
    }
}
