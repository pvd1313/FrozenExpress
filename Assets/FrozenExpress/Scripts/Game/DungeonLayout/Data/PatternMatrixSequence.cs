using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    [System.Serializable]
    public class PatternMatrixSequence
    {
        public IList<EPatternTransform> Sequence => _sequence;

        [SerializeField] private List<EPatternTransform> _sequence;
    }
}
