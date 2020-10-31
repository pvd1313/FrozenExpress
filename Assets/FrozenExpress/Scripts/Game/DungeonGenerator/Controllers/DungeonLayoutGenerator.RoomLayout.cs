using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class RoomLayout
        {
            public List<CellLayer> Layout;
            public List<PointDirection> Exits;
        }
    }
}