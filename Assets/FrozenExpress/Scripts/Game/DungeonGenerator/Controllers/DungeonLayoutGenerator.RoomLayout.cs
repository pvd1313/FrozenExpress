using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class RoomLayout
        {
            public List<CellLayer> Layout;
            public List<PointDirection> Exits;
            public List<Vector3Int> Bounds;
        }
    }
}