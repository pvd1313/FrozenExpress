using System;
using System.Linq;
using Bunny83.LSystem;
using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.Dungeon.Interfaces;

namespace FrozenExpress.Scripts.Game.Dungeon.Controllers
{
    public class DungeonPatternExpressionParser : IDungeonPatternExpressionParser
    {
        private readonly Parser _parser;
        private readonly ADebugLogger _debugLog;

        public DungeonPatternExpressionParser(ADebugFactory debugFactory)
        {
            _parser = new Parser();
            _debugLog = debugFactory.CreateLogger(this);
        }

        public LogicExpression CompileExpression(EntityPattern entityPattern)
        {
            LogicExpression expression;
            var context = new ExpressionContext(false);

            try
            {
                expression = _parser.Parse(entityPattern.Condition.Expression, context);
            }
            catch (Exception e)
            {
                _debugLog?.Exception(e, $"Failed to parse '{entityPattern.Condition.Expression}' in '{entityPattern.Name}'");
                return null;
            }

            var conditions = entityPattern.Condition.Conditions;
            var variables = expression.Context.variables;

            var duplicatedConditionNames = conditions.Select(c => c.Variable)
                .GroupBy(v => v)
                .Where(v => v.Count() > 1);
            foreach (var name in duplicatedConditionNames)
            {
                _debugLog?.Error($"Duplicated variable '{name.Key}' in '{entityPattern.Name}'");
                expression = null;
            }


            var missingNames = variables.Keys.Except(conditions.Select(c => c.Variable));
            foreach (var name in missingNames)
            {
                _debugLog?.Error($"Missing condition '{name}' in '{entityPattern.Name}'");
                expression = null;
            }

            return expression;
        }
    }
}
