using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonGenerator
{
    public partial class DungeonLayoutGenerator
    {
        private class BakedRoom
        {
            public RoomPattern Source { get; set; }
            public List<RoomLayout>[] Layouts { get; }

            public BakedRoom()
            {
                Layouts = new List<RoomLayout>[27];
            }
        }
    }
}