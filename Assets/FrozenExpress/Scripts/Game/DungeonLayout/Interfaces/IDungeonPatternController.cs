using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonPatternController
    {
        IEnumerable<DungeonEntity> IterateEntities(IDungeonMap layoutMap);
    }
}
