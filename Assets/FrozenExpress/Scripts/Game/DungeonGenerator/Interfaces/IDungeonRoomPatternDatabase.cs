using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonGenerator
{
    public interface IDungeonRoomPatternDatabase
    {
        IEnumerable<RoomPattern> All { get; }
    }
}