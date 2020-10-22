using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Enums;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Data
{
    [System.Serializable]
    public class PatternMatrixSequence
    {
        public IList<EPatternTransform> Sequence => _sequence;

        [SerializeField] private List<EPatternTransform> _sequence;
    }
}
