using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Assets
{
    public class DungeonPatternDatabaseAsset : ScriptableObject
    {
        public IReadOnlyList<EntityPattern> Patterns => _patterns;

        [SerializeField] private List<EntityPattern> _patterns;
    }
}
