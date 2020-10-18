namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Flags]
    public enum EDungeonLayoutPatternFlip
    {
        None = 0,
        Identity = 1,
        FlipX = 2,
        FlipY = 4,
        FlipZ = 8
    }
}
