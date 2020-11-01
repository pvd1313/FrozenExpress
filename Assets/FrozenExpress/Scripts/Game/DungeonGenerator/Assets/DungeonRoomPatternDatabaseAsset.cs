using System;
using System.Collections.Generic;
using FrozenExpress.Game.Serialization;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public class DungeonRoomPatternDatabaseAsset : AScriptableObjectDatabase
    {
        public IReadOnlyList<RoomPattern> Patterns => _patterns;

        [SerializeField] private List<RoomPattern> _patterns;
    }
}