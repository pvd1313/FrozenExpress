namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonEntityDatabase
    {
        DungeonEntityConfig GetEntity(string id);
    }
}
