using B83.LogicExpressionParser;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonPatternExpressionParser
    {
        LogicExpression CompileExpression(Pattern pattern);
    }
}
