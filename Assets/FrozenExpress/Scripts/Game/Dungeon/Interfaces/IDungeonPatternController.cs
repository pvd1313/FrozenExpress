using System.Collections.Generic;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonPatternController
    {
        IEnumerable<MeshTransform> IterateEntities(IDungeonMap layoutMap);
    }
}
