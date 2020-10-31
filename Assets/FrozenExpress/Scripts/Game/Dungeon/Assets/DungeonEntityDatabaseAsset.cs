using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonEntityDatabaseAsset : ScriptableObject
    {
        public event Action OnModified;

        public IReadOnlyList<DungeonEntity> Entities => _entities;

        [SerializeField] private List<DungeonEntity> _entities;

        private void OnValidate()
        {
            OnModified?.Invoke();
        }
    }
}
