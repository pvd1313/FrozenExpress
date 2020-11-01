using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class PatternMatrixSequence
    {
        public IReadOnlyList<EPatternTransform> Sequence => _sequence;

        [SerializeField] private List<EPatternTransform> _sequence;
    }
}
