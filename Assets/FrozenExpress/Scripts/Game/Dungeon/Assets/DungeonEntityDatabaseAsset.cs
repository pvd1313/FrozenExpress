using System.Collections.Generic;
using FrozenExpress.Game.Serialization;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonEntityDatabaseAsset : AScriptableObjectDatabase
    {
        public IReadOnlyList<DungeonEntity> Entities => _entities;

        [SerializeField] private List<DungeonEntity> _entities;
    }
}
