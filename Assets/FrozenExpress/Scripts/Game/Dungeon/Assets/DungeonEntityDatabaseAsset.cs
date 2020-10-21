using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonEntityDatabaseAsset : ScriptableObject
    {
        public int RuntimeSerializedVersion { get; private set; }

        public IReadOnlyList<DungeonEntityConfig> Entities => _entities;

        [SerializeField] private List<DungeonEntityConfig> _entities;

        private void OnValidate()
        {
            RuntimeSerializedVersion++;
        }
    }
}
