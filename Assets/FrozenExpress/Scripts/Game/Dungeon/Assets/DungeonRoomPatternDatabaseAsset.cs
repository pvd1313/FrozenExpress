using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonRoomPatternDatabaseAsset : ScriptableObject
    {
        public event Action OnModified;
        public IReadOnlyList<RoomPattern> Patterns => _patterns;

        [SerializeField] private List<RoomPattern> _patterns;

        private void OnValidate()
        {
            OnModified?.Invoke();
        }
    }
}