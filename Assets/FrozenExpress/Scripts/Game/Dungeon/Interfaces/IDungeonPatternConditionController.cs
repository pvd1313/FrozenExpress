using Bunny83.LSystem;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonPatternExpressionParser
    {
        LogicExpression CompileExpression(EntityPattern entityPattern);
    }
}
