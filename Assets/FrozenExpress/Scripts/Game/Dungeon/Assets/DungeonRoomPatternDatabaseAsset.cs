using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Assets
{
    public class DungeonRoomPatternDatabaseAsset : ScriptableObject
    {
        public IReadOnlyList<RoomPattern> Patterns => _patterns;

        [SerializeField] private List<RoomPattern> _patterns;
    }
}