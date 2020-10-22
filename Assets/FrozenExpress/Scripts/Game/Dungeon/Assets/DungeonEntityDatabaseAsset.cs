using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Assets
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
