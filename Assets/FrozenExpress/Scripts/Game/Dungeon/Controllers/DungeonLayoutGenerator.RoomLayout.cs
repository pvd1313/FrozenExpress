using System.Collections.Generic;

namespace FrozenExpress.Game.Dungeon
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