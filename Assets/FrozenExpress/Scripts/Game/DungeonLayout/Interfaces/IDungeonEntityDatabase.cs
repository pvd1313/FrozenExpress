namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonEntityDatabase
    {
        DungeonEntityConfig GetEntity(string id);
    }
}
