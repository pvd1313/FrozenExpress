using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonPatternDatabaseAsset : ScriptableObject
    {
        public IReadOnlyList<Pattern> Patterns => _patterns;

        [SerializeField] private List<Pattern> _patterns;
    }
}
