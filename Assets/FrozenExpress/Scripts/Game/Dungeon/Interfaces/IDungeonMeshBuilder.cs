using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(IReadOnlyDictionary<Vector3Int, int> dungeonLayout, Mesh result);
    }
}
