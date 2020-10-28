using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class EntityPattern
    {
        public string Name => _name;
        public string EntityId => _entityId;
        public EPatternTrigger Trigger => _trigger;
        public IReadOnlyList<PatternMatrixSequence> MatrixSequences => _matrixSequences;
        public PatternConditionExpression Condition => _condition;

        public override string ToString()
        {
            return Name;
        }

        [SerializeField] private string _name;
        [SerializeField] private string _entityId;
        [SerializeField] private EPatternTrigger _trigger;
        [SerializeField] [OneLine] private List<PatternMatrixSequence> _matrixSequences;
        [SerializeField] private PatternConditionExpression _condition;
    }
}
