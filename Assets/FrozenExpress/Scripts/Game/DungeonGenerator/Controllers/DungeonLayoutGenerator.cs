using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Dungeon;
using UnityEngine;

using GreatRandom = UnityEngine.Random;

namespace FrozenExpress.Game.DungeonGenerator
{
    public partial class DungeonLayoutGenerator : IDungeonLayoutGenerator
    {
        private readonly IDungeonRoomPatternProcessor _roomPatternProcessor;
        private readonly IDungeonRoomPatternDatabase _patternDatabase;
        private readonly DungeonLayoutGeneratorConfig _config;
        private readonly IDungeonPatternTransformController _patternTransformController;
        private readonly ADebugLogger _debugLog;
        private readonly RoomBaker _baker;
        private readonly RoomPlacer _placer;
        private readonly RoomConnectionController _roomConnection;
        
        private readonly List<BakedRoom> _roomsAll;
        private readonly List<BakedRoom> _roomsBuffer;
        private readonly List<PointDirection> _worldExits;
        private readonly Dictionary<Vector3Int, string> _worldExitConnectors;

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
            _roomConnection = new RoomConnectionController();
            _debugLog = debugFactory?.CreateLogger(this);
            _baker = new RoomBaker(this);
            _placer = new RoomPlacer(this);
            
            _roomsAll = new List<BakedRoom>();
            _roomsBuffer = new List<BakedRoom>();
            _worldExits = new List<PointDirection>();
            _worldExitConnectors = new Dictionary<Vector3Int, string>();
        }

        public void GenerateDungeon(int cellCount, IDictionary<Vector3Int, int> result)
        {
            _roomConnection.Clear();
            foreach (var rule in _config.ConnectionRules)
            {
                _roomConnection.AllowConnection(rule.SourceConnector, rule.DestinationConnector);
            }
            
            _worldMap = result;
            _worldMap.Clear();
            
            _roomsAll.Clear();
            foreach (var roomPattern in _patternDatabase.All)
            {
                var bakedRoom = _baker.TryBake(roomPattern);
                if (bakedRoom != null)
                {
                    _roomsAll.Add(bakedRoom);
                    _roomConnection.AddRoom(bakedRoom);
                }
            }
            
            if (_roomsAll.Count == 0)
            {
                _debugLog?.Error("No rooms to generate dungeon");
                return;
            }
            
            _worldExits.Clear();
            _worldExitConnectors.Clear();
            
            var firstRoomIndex = GreatRandom.Range(0, _roomsAll.Count);
            _placer.PlaceFirstRoom(_roomsAll[firstRoomIndex]);

            while (_worldMap.Count < cellCount && _worldExits.Count > 0)
            {
                var exitIndex = GreatRandom.Range(0, _worldExits.Count);
                var worldExit = _worldExits[exitIndex];
                _worldExits.RemoveAt(exitIndex);

                var exitConnector = _worldExitConnectors[worldExit.Point];
                _worldExitConnectors.Remove(worldExit.Point);
                
                var availableRooms = _roomConnection.FindAvailableRooms(exitConnector);
                _roomsBuffer.Clear();
                _roomsBuffer.AddRange(availableRooms);

                while (_roomsBuffer.Count > 0)
                {
                    var roomIndex = GreatRandom.Range(0, _roomsBuffer.Count);
                    var room = _roomsBuffer[roomIndex];
                    _roomsBuffer.RemoveAt(roomIndex);

                    if (_placer.TryPlaceRoom(room, worldExit))
                    {
                        break;
                    }
                }
            }

            foreach (var room in _roomsAll)
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