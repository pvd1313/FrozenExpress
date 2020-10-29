using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using UnityEngine;

using GreatRandom = UnityEngine.Random;

namespace FrozenExpress.Game.Dungeon
{
    public partial class DungeonLayoutGenerator : IDungeonLayoutGenerator
    {
        private class BakedRoom
        {
            public RoomPattern Source { get; set; }
            public List<RoomLayout>[] Layouts { get; }

            public BakedRoom()
            {
                Layouts = new List<RoomLayout>[27];
            }
        }
        
        private readonly IDungeonRoomPatternProcessor _roomPatternProcessor;
        private readonly IDungeonRoomPatternDatabase _patternDatabase;
        private readonly DungeonLayoutGeneratorConfig _config;
        private readonly IDungeonPatternTransformController _patternTransformController;
        private readonly ADebugLogger _debugLog;
        private readonly RoomBaker _baker;
        private readonly RoomPlacer _placer;
        
        private readonly List<BakedRoom> _roomsFresh;
        private readonly List<BakedRoom> _roomsUsed;
        private readonly List<PointDirection> _worldExits;

        private IDictionary<Vector3Int, int> _worldMap;

        public DungeonLayoutGenerator(IDungeonRoomPatternProcessor roomPatternProcessor,
                                      IDungeonRoomPatternDatabase patternDatabase,
                                      DungeonLayoutGeneratorConfig config,
                                      ADebugFactory debugFactory,
                                      IDungeonPatternTransformController patternTransformController)
        {
            _roomPatternProcessor = roomPatternProcessor;
            _patternDatabase = patternDatabase;
            _config = config;
            _patternTransformController = patternTransformController;
            _debugLog = debugFactory?.CreateLogger(this);
            _baker = new RoomBaker(this);
            _placer = new RoomPlacer(this);
            
            _roomsFresh = new List<BakedRoom>();
            _roomsUsed = new List<BakedRoom>();
            _worldExits = new List<PointDirection>();
        }

        public void GenerateDungeon(int cellCount, IDictionary<Vector3Int, int> result)
        {
            _worldMap = result;
            _worldMap.Clear();
            
            _roomsFresh.Clear();
            _roomsUsed.Clear();
            foreach (var roomPattern in _patternDatabase.All)
            {
                var bakedRoom = _baker.TryBake(roomPattern);
                if (bakedRoom != null)
                {
                    _roomsFresh.Add(bakedRoom);
                }
            }
            
            if (_roomsFresh.Count == 0)
            {
                _debugLog?.Error("No rooms to generate dungeon");
                return;
            }
            
            _worldExits.Clear();
            
            var firstRoomIndex = GreatRandom.Range(0, _roomsFresh.Count);
            _placer.PlaceFirstRoom(_roomsFresh[firstRoomIndex]);

            while (_worldMap.Count < cellCount && _worldExits.Count > 0)
            {
                var exitIndex = GreatRandom.Range(0, _worldExits.Count);
                var worldExit = _worldExits[exitIndex];
                _worldExits.RemoveAt(exitIndex);

                while (_roomsFresh.Count > 0)
                {
                    var roomIndex = GreatRandom.Range(0, _roomsFresh.Count);
                    var room = _roomsFresh[roomIndex];
                    _roomsUsed.Add(room);
                    _roomsFresh.RemoveAt(roomIndex);

                    if (_placer.TryPlaceRoom(room, worldExit))
                    {
                        break;
                    }
                }
                
                _roomsFresh.AddRange(_roomsUsed);
                _roomsUsed.Clear();
            }
            
            _roomsFresh.AddRange(_roomsUsed);
            foreach (var room in _roomsFresh)
            {
                _baker.Dispose(room);
            }

            if (_worldMap.Count < cellCount)
            {
                _debugLog?.Error("Failed to create full size dungeon");
            }
        }
        
        private static int DirectionToIndex(Vector3Int direction)
        {
            return 9 * (direction.x + 1) + 3 * (direction.y + 1) + direction.z + 1;
        }
    }
}