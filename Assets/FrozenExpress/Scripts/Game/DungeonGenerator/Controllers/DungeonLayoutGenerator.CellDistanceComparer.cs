using System;
using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class CellDistanceComparer : IComparer<CellLayer>, IComparer<Vector3Int>
        {
            public int Compare(CellLayer a, CellLayer b)
            {
                return Compare(a.Cell, b.Cell);
            }

            public int Compare(Vector3Int a, Vector3Int b)
            {
                var lenA = Math.Abs(a.x) + Math.Abs(a.y) + Math.Abs(a.z);
                var lenB = Math.Abs(b.x) + Math.Abs(b.y) + Math.Abs(b.z);

                return lenA - lenB;
            }
        }
    }
}