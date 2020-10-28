using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Assets
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