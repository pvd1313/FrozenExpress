using Bunny83.LSystem;
using FrozenExpress.Scripts.Game.Dungeon.Data;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonPatternExpressionParser
    {
        LogicExpression CompileExpression(EntityPattern entityPattern);
    }
}
