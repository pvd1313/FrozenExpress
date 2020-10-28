using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonRoomPatternDatabase
    {
        IEnumerable<RoomPattern> All { get; }
    }
}