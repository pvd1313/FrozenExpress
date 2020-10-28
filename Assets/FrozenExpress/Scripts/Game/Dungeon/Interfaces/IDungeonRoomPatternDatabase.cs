using System.Collections.Generic;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonRoomPatternDatabase
    {
        IEnumerable<RoomPattern> All { get; }
    }
}