using System;
using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class CellDistanceComparer : IComparer<CellLayer>
        {
            public int Compare(CellLayer x, CellLayer y)
            {
                var cellA = x.Cell;
                var cellB = y.Cell;
                var valueA = Math.Abs(cellA.x) + Math.Abs(cellA.y) + Math.Abs(cellA.z);
                var valueB = Math.Abs(cellB.x) + Math.Abs(cellB.y) + Math.Abs(cellB.z);
                    
                return valueA - valueB;
            }
        }
    }
}