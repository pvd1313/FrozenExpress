using System;
using System.Collections.Generic;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonEntityPatternDatabase
    {
        event Action OnCashUpdate;
        
        IEnumerable<EntityPattern> All { get; }
    }
}