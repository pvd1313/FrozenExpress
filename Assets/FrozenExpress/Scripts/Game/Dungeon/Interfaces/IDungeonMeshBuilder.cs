using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(IReadOnlyDictionary<Vector3Int, int> dungeonLayout, Mesh result);
    }
}
