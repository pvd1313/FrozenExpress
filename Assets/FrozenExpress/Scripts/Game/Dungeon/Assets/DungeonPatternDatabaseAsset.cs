using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonPatternDatabaseAsset : ScriptableObject
    {
        public IReadOnlyList<EntityPattern> Patterns => _patterns;

        [SerializeField] private List<EntityPattern> _patterns;
    }
}
