using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Serializable]
    public class Pattern
    {
        public string Name => _name;
        public EPatternTrigger Trigger => _trigger;
        public Mesh Mesh => _mesh;
        public IReadOnlyList<PatternMatrixSequence> MatrixSequences => _matrixSequences;
        public IReadOnlyList<PatternCondition> Conditions => _conditions;

        [SerializeField] private string _name;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private EPatternTrigger _trigger;
        [SerializeField] private List<PatternMatrixSequence> _matrixSequences;
        [SerializeField] private List<PatternCondition> _conditions;
    }
}
