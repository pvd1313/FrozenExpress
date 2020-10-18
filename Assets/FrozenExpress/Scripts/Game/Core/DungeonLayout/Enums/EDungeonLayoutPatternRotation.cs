namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Flags]
    public enum EDungeonLayoutPatternRotation
    {
        None = 0,
        Identity = 1,
        YAxis90 = 2,
        YAxis180 = 4,
        YAxis270 = 8,
    }
}
