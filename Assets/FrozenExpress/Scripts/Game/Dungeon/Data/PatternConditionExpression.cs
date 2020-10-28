using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class PatternConditionExpression
    {
        public IReadOnlyList<PatternCondition> Conditions => _conditions;
        public string Expression => _expression;

        [SerializeField] string _expression;
        [SerializeField] [OneLine] List<PatternCondition> _conditions;
    }
}