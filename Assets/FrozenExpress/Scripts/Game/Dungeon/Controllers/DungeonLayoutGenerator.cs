using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.Dungeon.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Controllers
{
    public class DungeonLayoutGenerator : IDungeonLayoutGenerator
    {
        private readonly IDungeonRoomPatternProcessor _roomPatternProcessor;
        private readonly List<RoomPattern> _acceptedPatterns;

        public DungeonLayoutGenerator(IDungeonRoomPatternProcessor roomPatternProcessor)
        {
            _roomPatternProcessor = roomPatternProcessor;
            _acceptedPatterns = new List<RoomPattern>();
        }

        public void GenerateDungeon(int seed, Dictionary<Vector3Int, int> result)
        {
            result.Clear();
        }
    }
}