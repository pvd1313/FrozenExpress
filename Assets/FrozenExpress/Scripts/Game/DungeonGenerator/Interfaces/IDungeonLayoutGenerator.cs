using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public interface IDungeonLayoutGenerator
    {
        void GenerateDungeon(DungeonPreset dungeonPreset, IDictionary<Vector3Int, int> result);
    }
}