using B83.LogicExpressionParser;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonPatternExpressionParser
    {
        LogicExpression CompileExpression(Pattern pattern);
    }
}
