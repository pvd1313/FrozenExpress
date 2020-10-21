using B83.LogicExpressionParser;
using System;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonPatternExpressionParser : IDungeonPatternExpressionParser
    {
        private readonly Parser _parser;
        private readonly ALogger _log;

        public DungeonPatternExpressionParser(ALogFactory logFactory)
        {
            _parser = new Parser();
            _log = logFactory.Create(this);
        }

        public LogicExpression CompileExpression(Pattern pattern)
        {
            LogicExpression expression;
            var context = new ExpressionContext(false);

            try
            {
                expression = _parser.Parse(pattern.Condition.Expression, context);
            }
            catch (Exception e)
            {
                _log?.Exception(e, $"Failed to parse expression '{pattern.Condition.Expression}' in pattern '{pattern.Name}'");
                return null;
            }

            var conditions = pattern.Condition.Conditions;
            var variables = expression.Context.variables;

            var duplicatedConditionNames = conditions.Select(c => c.Variable)
                .GroupBy(v => v)
                .Where(v => v.Count() > 1);
            foreach (var name in duplicatedConditionNames)
            {
                _log?.Error($"Condition variable '{name.Key}' is duplicated");
                expression = null;
            }


            var missingNames = variables.Keys.Except(conditions.Select(c => c.Variable));
            foreach (var name in missingNames)
            {
                _log?.Error($"Variable mistmatch '{name}' in pattern '{pattern.Name}'");
                expression = null;
            }

            var overNames = conditions.Select(c => c.Variable).Except(variables.Keys);
            foreach (var name in overNames)
            {
                _log?.Error($"Variable mistmatch '{name}' in pattern '{pattern.Name}'");
                expression = null;
            }

            return expression;
        }
    }
}
