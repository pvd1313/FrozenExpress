using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Serializable]
    public class PatternMatrixSequence
    {
        public IList<EPatternMatrixTransform> Sequence => _sequence;

        [SerializeField] private List<EPatternMatrixTransform> _sequence;
    }
}
