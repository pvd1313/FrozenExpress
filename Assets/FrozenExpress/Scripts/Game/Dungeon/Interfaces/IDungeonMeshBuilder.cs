using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(DungeonLayout dungeonLayout, Mesh result);
    }
}
