using System;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [Flags]
    public enum EDungeonLayoutPatternRotation
    {
        None = 0,
        Identity = 1,
        Up90 = 2,
        Up180 = 4,
        Up270 = 8,
    }
}
