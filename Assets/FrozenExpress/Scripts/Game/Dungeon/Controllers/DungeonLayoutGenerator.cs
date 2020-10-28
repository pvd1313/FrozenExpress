using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using UnityEngine;

using GreatRandom = UnityEngine.Random;

namespace FrozenExpress.Game.Dungeon
{
    public partial class DungeonLayoutGenerator : IDungeonLayoutGenerator
    {
        private struct RoomPreset
        {
            public RoomPattern Pattern;
            public List<PointDirection> ExitPoints;
        }
        
        private readonly IDungeonRoomPatternProcessor _roomPatternProcessor;
        private readonly IDungeonRoomPatternDatabase _patternDatabase;
        private readonly DungeonLayoutGeneratorConfig _config;
        private readonly ADebugLogger _debugLog;
        private readonly PresetGenerator _presetGenerator;
        private readonly LayoutModifier _layoutModifier;

        private readonly List<RoomPreset> _roomsFresh;
        private readonly List<RoomPreset> _roomsUsed;
        private readonly List<PointDirection> _connections;

        private IDictionary<Vector3Int, int> _layout;

        public DungeonLayoutGenerator(IDungeonRoomPatternProcessor roomPatternProcessor,
                                      IDungeonRoomPatternDatabase patternDatabase,
                                      DungeonLayoutGeneratorConfig config,
                                      ADebugFactory debugFactory)
        {
            _roomPatternProcessor = roomPatternProcessor;
            _patternDatabase = patternDatabase;
            _config = config;
            _layoutModifier = new LayoutModifier(this);
            _presetGenerator = new PresetGenerator(this);
            _debugLog = debugFactory?.CreateLogger(this);
            
            _roomsFresh = new List<RoomPreset>();
            _roomsUsed = new List<RoomPreset>();
            _connections = new List<PointDirection>();
        }

        public void GenerateDungeon(int cellCount, IDictionary<Vector3Int, int> result)
        {
            _layout = result;
            _layout.Clear();
            
            _presetGenerator.Prepare(_patternDatabase.All, _roomsFresh);
            if (_roomsFresh.Count == 0)
            {
                _debugLog?.Error("No room presets to generate dungeon");
                return;
            }

            _connections.Clear();

            var firstRoomIndex = GreatRandom.Range(0, _roomsFresh.Count);
            _layoutModifier.SpawnFirstRoom(_roomsFresh[firstRoomIndex]);
            
            while (_layout.Count < cellCount && _connections.Count > 0)
            {
                var pointIndex = GreatRandom.Range(0, _connections.Count);
                var worldConnection = _connections[pointIndex];
                _connections.RemoveAt(pointIndex);

                while (_roomsFresh.Count > 0)
                {
                    var roomIndex = GreatRandom.Range(0, _roomsFresh.Count);
                    var room = _roomsFresh[roomIndex];
                    _roomsUsed.Add(room);
                    _roomsFresh.RemoveAt(roomIndex);

                    if (_layoutModifier.TrySpawnRoom(worldConnection, room))
                    {
                        break;
                    }
                }
                
                _roomsFresh.AddRange(_roomsUsed);
                _roomsUsed.Clear();
            }

            if (_layout.Count < cellCount)
            {
                _debugLog?.Error("Failed to create full size dungeon");
            }
        }
    }
}