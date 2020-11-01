using System;
using System.Collections.Generic;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonEntityDatabase
    {
        event Action OnCashUpdate;

        IEnumerable<DungeonEntity> All { get; }
        DungeonEntity GetEntity(string id);
    }
}
