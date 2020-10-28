using System.Collections.Generic;
using UnityEngine;

using GreatRandom = UnityEngine.Random;

namespace FrozenExpress.Game.Dungeon
{
    public partial class DungeonLayoutGenerator
    {
        private class LayoutModifier
        {
            private static readonly Vector3Int Vector3IntZero = new Vector3Int(0, 0, 0);
            
            private readonly DungeonLayoutGenerator _owner;
            private readonly List<PointDirection> _exitPointBuffer;
            private readonly List<CellLayer> _worldCellBuffer;

            public LayoutModifier(DungeonLayoutGenerator owner)
            {
                _owner = owner;
                _exitPointBuffer = new List<PointDirection>();
                _worldCellBuffer = new List<CellLayer>();
            }

            public bool TrySpawnRoom(PointDirection worldExit, RoomPreset room)
            {
                if (!TryFindConnection(worldExit, room, out var roomExit))
                {
                    return false;
                }

                var layout = _owner._layout;
                var roomCells = room.Pattern.Cells;
                
                _worldCellBuffer.Clear();

                var roomExitIndex = -1;
                
                for (var i = 0; i < roomCells.Count; i++)
                {
                    var cell = roomCells[i];
                    if (roomExitIndex < 0 && cell.Cell == roomExit.Point)
                    {
                        roomExitIndex = i;
                        continue;
                    }
                    
                    CellLayer worldCell;
                    worldCell.Cell = worldExit.Point - roomExit.Point + cell.Cell;
                    worldCell.Layer = cell.Layer;
                    _worldCellBuffer.Add(worldCell);
                    
                    if (layout.ContainsKey(worldCell.Cell))
                    {
                        return false;
                    }
                }

                layout[worldExit.Point] |= roomCells[roomExitIndex].Layer;
                for (var i = 0; i < _worldCellBuffer.Count; i++)
                {
                    var cell = _worldCellBuffer[i];
                    layout[cell.Cell] = cell.Layer;
                }

                var connections = _owner._connections;
                var roomExits = room.ExitPoints;
                for (var i = 0; i < roomExits.Count; i++)
                {
                    var worldExitNew = roomExits[i];
                    worldExitNew.Point = worldExit.Point - roomExit.Point + worldExitNew.Point;
                    connections.Add(worldExitNew);
                }

                return true;
            }

            private bool TryFindConnection(PointDirection worldConnection, RoomPreset room, out PointDirection result)
            {
                result = default;
                
                var roomExitPoints = room.ExitPoints;
                _exitPointBuffer.Clear();
                
                for (var i = 0; i < roomExitPoints.Count; i++)
                {
                    var tempExitPoint = roomExitPoints[i];
                    if (tempExitPoint.Direction + worldConnection.Direction == Vector3IntZero)
                    {
                        _exitPointBuffer.Add(tempExitPoint);
                    }
                }
                
                if (_exitPointBuffer.Count == 0)
                {
                    return false;
                }
                
                var pointIndex = GreatRandom.Range(0, _exitPointBuffer.Count);
                result = _exitPointBuffer[pointIndex];

                return true;
            }
            
            public void SpawnFirstRoom(RoomPreset room)
            {
                var layout = _owner._layout;
                var cells = room.Pattern.Cells;
                for (var i = 0; i < cells.Count; i++)
                {
                    var cellData = cells[i];
                    layout[cellData.Cell] = cellData.Layer;
                }
                
                _owner._connections.AddRange(room.ExitPoints);
            }
        }
    }
}