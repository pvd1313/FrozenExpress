using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonEntityDatabaseAsset : ScriptableObject
    {
        public event Action OnModified;

        public IReadOnlyList<DungeonEntityConfig> Entities => _entities;

        [SerializeField] private List<DungeonEntityConfig> _entities;

        private void OnValidate()
        {
            OnModified?.Invoke();
        }
    }
}
