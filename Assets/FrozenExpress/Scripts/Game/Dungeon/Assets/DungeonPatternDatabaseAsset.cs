using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Assets
{
    public class DungeonPatternDatabaseAsset : ScriptableObject
    {
        public IReadOnlyList<Pattern> Patterns => _patterns;

        [SerializeField] private List<Pattern> _patterns;
    }
}
