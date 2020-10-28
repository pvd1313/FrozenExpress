using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonLayoutGenerator
    {
        void GenerateDungeon(int seed, Dictionary<Vector3Int, int> result);
    }
}