using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonLayoutGenerator
    {
        void GenerateDungeon(int seed, Dictionary<Vector3Int, int> result);
    }
}