using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutPatternCollectionAsset : ScriptableObject
    {
        public IReadOnlyList<DungeonLayoutPatternData> Patterns => _patterns;

        [SerializeField] private List<DungeonLayoutPatternData> _patterns;
    }
}
