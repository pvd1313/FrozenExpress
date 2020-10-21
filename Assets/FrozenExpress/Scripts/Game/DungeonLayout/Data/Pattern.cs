using OneLine;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    [System.Serializable]
    public class Pattern
    {
        public string Name => _name;
        public EPatternTrigger Trigger => _trigger;
        public Mesh Mesh => _mesh;
        public IReadOnlyList<PatternMatrixSequence> MatrixSequences => _matrixSequences;
        public PatternConditionExpression Condition => _condition;

        [SerializeField] private string _name;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private EPatternTrigger _trigger;
        [SerializeField] [OneLine] private List<PatternMatrixSequence> _matrixSequences;
        [SerializeField] private PatternConditionExpression _condition;
    }
}
