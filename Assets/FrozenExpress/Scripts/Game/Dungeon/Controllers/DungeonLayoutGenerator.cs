using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
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