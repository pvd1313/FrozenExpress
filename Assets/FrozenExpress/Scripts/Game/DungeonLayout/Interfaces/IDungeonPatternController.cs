using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonPatternController
    {
        IEnumerable<MeshTransform> IterateEntities(IDungeonMap layoutMap);
    }
}
