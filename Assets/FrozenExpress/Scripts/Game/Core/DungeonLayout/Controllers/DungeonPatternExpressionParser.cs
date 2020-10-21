using B83.LogicExpressionParser;
using System;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonPatternExpressionParser : IDungeonPatternExpressionParser
    {
        private readonly Parser _parser;

        public DungeonPatternExpressionParser()
        {
            _parser = new Parser();
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
                LogError($"Failed to parse expression '{pattern.Condition.Expression}' in pattern '{pattern.Name}'");
                LogException(e);
                return null;
            }

            var conditions = pattern.Condition.Conditions;
            var variables = expression.Context.variables;

            var duplicatedConditionNames = conditions.Select(c => c.Variable)
                .GroupBy(v => v)
                .Where(v => v.Count() > 1);
            foreach (var name in duplicatedConditionNames)
            {
                LogError($"Condition variable '{name.Key}' is duplicated");
                expression = null;
            }


            var missingNames = variables.Keys.Except(conditions.Select(c => c.Variable));
            foreach (var name in missingNames)
            {
                LogError($"Variable mistmatch '{name}' in pattern '{pattern.Name}'");
                expression = null;
            }

            var overNames = conditions.Select(c => c.Variable).Except(variables.Keys);
            foreach (var name in overNames)
            {
                LogError($"Variable mistmatch '{name}' in pattern '{pattern.Name}'");
                expression = null;
            }

            return expression;
        }

        private void LogError(string message)
        {
            Debug.LogError($"{GetType().Name} :: {message}");
        }

        private void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
