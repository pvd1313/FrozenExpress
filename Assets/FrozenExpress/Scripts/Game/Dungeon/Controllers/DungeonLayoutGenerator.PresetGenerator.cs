using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public partial class DungeonLayoutGenerator
    {
        private class PresetGenerator
        {
            private readonly DungeonLayoutGenerator _owner;
            private readonly List<PointDirection> _exitPointBuffer;
            private readonly Dictionary<Vector3Int, int> _roomLayout;
            
            public PresetGenerator(DungeonLayoutGenerator owner)
            {
                _owner = owner;
                _exitPointBuffer = new List<PointDirection>();
                _roomLayout = new Dictionary<Vector3Int, int>();
            }

            public void Prepare(IEnumerable<RoomPattern> rooms, List<RoomPreset> result)
            {
                var config = _owner._config;
                var debugLog = _owner._debugLog;
                var roomPatternProcessor = _owner._roomPatternProcessor;
                
                result.Clear();
                
                foreach (var roomPattern in rooms)
                {
                    var cells = roomPattern.Cells;
                
                    if (cells.Count < config.RequiredRoomCellCount)
                    {
                        debugLog?.Error($"Room '{roomPattern.Id}' has less than {config.RequiredRoomCellCount} cells");
                        continue;
                    }

                    var roomLayoutGood = true;
                    _roomLayout.Clear();
                    for (var i = 0; i < cells.Count; i++)
                    {
                        var cellLayer = roomPattern.Cells[i];
                        if (_roomLayout.ContainsKey(cellLayer.Cell))
                        {
                            debugLog?.Error($"Room '{roomPattern.Id}' has duplicated cell {cellLayer.Cell} at index {i}");
                            roomLayoutGood = false;
                            break;
                        }
                        
                        _roomLayout.Add(cellLayer.Cell, cellLayer.Layer);
                    }
                    if (!roomLayoutGood)
                    {
                        continue;
                    }

                    var allCellsConnected = roomPatternProcessor.CheckAllCellsConnected(_roomLayout);
                    if (!allCellsConnected)
                    {
                        debugLog?.Error($"Room '{roomPattern.Id}' - not all cells are connected");
                        continue;
                    }
                
                    _exitPointBuffer.Clear();
                    roomPatternProcessor.FindConnectionPoints(_roomLayout, _exitPointBuffer);
                    if (_exitPointBuffer.Count < config.RequiredExitPointCount)
                    {
                        debugLog?.Error($"Room '{roomPattern.Id} has less than {config.RequiredExitPointCount} exit points");
                        continue;
                    }

                    RoomPreset room;
                    room.Pattern = roomPattern;
                    room.ExitPoints = new List<PointDirection>(_exitPointBuffer);
                    
                    result.Add(room);
                }
            }
        }
    }
}