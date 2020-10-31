using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public interface IDungeonLayoutGenerator
    {
        void GenerateDungeon(int cellCount, IDictionary<Vector3Int, int> result);
    }
}