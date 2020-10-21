using B83.LogicExpressionParser;

namespace FrozenExpress.Game.DungeonLayout
{
    public interface IDungeonPatternExpressionParser
    {
        LogicExpression CompileExpression(Pattern pattern);
    }
}
