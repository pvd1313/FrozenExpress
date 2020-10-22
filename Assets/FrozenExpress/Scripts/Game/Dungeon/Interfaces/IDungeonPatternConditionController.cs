using B83.LogicExpressionParser;
using FrozenExpress.Scripts.Game.Dungeon.Data;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonPatternExpressionParser
    {
        LogicExpression CompileExpression(Pattern pattern);
    }
}
