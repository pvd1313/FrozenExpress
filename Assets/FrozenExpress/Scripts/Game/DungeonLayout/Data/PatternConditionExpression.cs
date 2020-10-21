using OneLine;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    [System.Serializable]
    public class PatternConditionExpression
    {
        public IReadOnlyList<PatternCondition> Conditions => _conditions;
        public string Expression => _expression;

        [SerializeField] string _expression;
        [SerializeField] [OneLineWithHeader] List<PatternCondition> _conditions;
    }
}
