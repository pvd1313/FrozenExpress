using System.Collections.Generic;
using FrozenExpress.Game.Serialization;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonPatternDatabaseAsset : AScriptableObjectDatabase
    {
        public IReadOnlyList<EntityPattern> Patterns => _patterns;

        [SerializeField] private List<EntityPattern> _patterns;
    }
}
