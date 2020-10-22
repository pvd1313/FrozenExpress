using FrozenExpress.Scripts.Game.Dungeon.Data;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonEntityDatabase
    {
        DungeonEntityConfig GetEntity(string id);
    }
}
