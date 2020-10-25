using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Assets
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
