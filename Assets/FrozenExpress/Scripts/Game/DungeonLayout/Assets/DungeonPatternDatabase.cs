using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonPatternDatabase : ScriptableObject
    {
        public IReadOnlyList<Pattern> Patterns => _patterns;

        [SerializeField] private List<Pattern> _patterns;
    }
}
