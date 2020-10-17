using System.Collections.Generic;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonLayoutPatternMaster
    {
        IEnumerable<DungeonLayoutEntityMesh> IterateEntities(IDungeonLayoutMap layoutMap);
    }
}
