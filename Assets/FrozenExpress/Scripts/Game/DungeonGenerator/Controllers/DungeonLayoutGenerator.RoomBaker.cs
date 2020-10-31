using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class RoomBaker
        {
            private readonly DungeonLayoutGenerator _owner;

            private readonly ItemPool<BakedRoom> _roomPool;
            private readonly ItemPool<List<CellLayer>> _cellListPool;
            private readonly ItemPool<List<PointDirection>> _directionListPool;
            private readonly ItemPool<RoomLayout> _roomLayoutPool;
            private readonly ItemPool<List<RoomLayout>> _roomLayoutListPool;
            private readonly CellDistanceComparer _cellDistanceComparer;

            private readonly Dictionary<Vector3Int, int> _layoutBufferMap;
            private readonly List<PointDirection> _exitPointBuffer;
            private readonly List<CellLayer> _exitLayoutCells;
            private readonly List<PointDirection> _exitLayoutExits;

            public RoomBaker(DungeonLayoutGenerator owner)
            {
                _owner = owner;
                _cellDistanceComparer = new CellDistanceComparer();
                _roomPool = new ItemPool<BakedRoom>();
                _cellListPool = new ItemPool<List<CellLayer>>();
                _roomLayoutPool = new ItemPool<RoomLayout>();
                _roomLayoutListPool = new ItemPool<List<RoomLayout>>();
                _directionListPool = new ItemPool<List<PointDirection>>();
                _layoutBufferMap = new Dictionary<Vector3Int, int>();
                _exitLayoutCells = new List<CellLayer>();
                _exitLayoutExits = new List<PointDirection>();
                _exitPointBuffer = new List<PointDirection>();
            }

            public BakedRoom TryBake(RoomPattern room)
            {
                var config = _owner._config;
                var debugLog = _owner._debugLog;
                var roomPatternProcessor = _owner._roomPatternProcessor;

                if (room.Matrix.Count == 0)
                {
                    debugLog?.Error($"Room '{room.Id}' has no matrix");
                    return null;
                }
                
                var cells = room.Cells;
                if (cells.Count < config.RequiredRoomCellCount)
                {
                    debugLog?.Error($"Room '{room.Id}' has less than {config.RequiredRoomCellCount} cells");
                    return null;
                }

                _layoutBufferMap.Clear();
                for (var i = 0; i < cells.Count; i++)
                {
                    var cellLayer = cells[i];
                    if (_layoutBufferMap.ContainsKey(cellLayer.Cell))
                    {
                        debugLog?.Error($"Room '{room.Id}' has duplicated cell {cellLayer.Cell} at index {i}");
                        return null;
                    }
                    
                    _layoutBufferMap.Add(cellLayer.Cell, cellLayer.Layer);
                }

                if (!roomPatternProcessor.CheckAllCellsConnected(_layoutBufferMap))
                {
                    debugLog?.Error($"Room '{room.Id}' - not all cells are connected");
                    return null;
                }
                
                roomPatternProcessor.FindConnectionPoints(_layoutBufferMap, _exitPointBuffer);
                if (_exitPointBuffer.Count < config.RequiredExitPointCount)
                {
                    debugLog?.Error($"Room '{room.Id} has less than {config.RequiredExitPointCount} exit points");
                    return null;
                }

                var bakedRoom = _roomPool.GetOrCreate();
                bakedRoom.Source = room;
                
                for (var i = 0; i < _exitPointBuffer.Count; i++)
                {
                    var exitPoint = _exitPointBuffer[i];
                    _exitLayoutCells.Clear();
                    
                    for (var j = 0; j < cells.Count; j++)
                    {
                        CellLayer cell;
                        cell.Cell = cells[j].Cell - exitPoint.Point;
                        cell.Layer = cells[j].Layer;
                        _exitLayoutCells.Add(cell);
                    }
                    
                    _exitLayoutCells.Sort(_cellDistanceComparer);
                    
                    _exitLayoutExits.Clear();
                    for (var j = 0; j < _exitPointBuffer.Count; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }

                        PointDirection direction;
                        direction.Point = _exitPointBuffer[j].Point - exitPoint.Point;
                        direction.Direction = _exitPointBuffer[j].Direction;
                        _exitLayoutExits.Add(direction);
                    }
                    
                    for (var j = 0; j < room.Matrix.Count; j++)
                    {
                        var matrix = room.Matrix[j];
                        var rotatedDirection = TransformPoint(exitPoint.Direction, matrix);
                        var directionIndex = DirectionToIndex(rotatedDirection);
                        var layoutsAtThisDirection = bakedRoom.Layouts[directionIndex];
                        if (layoutsAtThisDirection == null)
                        {
                            layoutsAtThisDirection = _roomLayoutListPool.GetOrCreate();
                            bakedRoom.Layouts[directionIndex] = layoutsAtThisDirection;
                        }

                        var currentLayoutMap = _cellListPool.GetOrCreate();
                        var currentLayoutExits = _directionListPool.GetOrCreate();
                        currentLayoutMap.Capacity = _exitLayoutCells.Count;
                        currentLayoutExits.Capacity = _exitLayoutExits.Count;

                        for (var q = 0; q < _exitLayoutCells.Count; q++)
                        {
                            var sourceCell = _exitLayoutCells[q];
                            CellLayer rotatedCell;
                            rotatedCell.Cell = TransformPoint(sourceCell.Cell, matrix);
                            rotatedCell.Layer = sourceCell.Layer;
                            currentLayoutMap.Add(rotatedCell);
                        }

                        for (var q = 0; q < _exitLayoutExits.Count; q++)
                        {
                            var sourceExit = _exitLayoutExits[q];
                            PointDirection rotatedExit;
                            rotatedExit.Point = TransformPoint(sourceExit.Point, matrix);
                            rotatedExit.Direction = TransformPoint(sourceExit.Direction, matrix);
                            currentLayoutExits.Add(rotatedExit);
                        }
                        
                        var currentLayout = _roomLayoutPool.GetOrCreate();
                        currentLayout.Layout = currentLayoutMap;
                        currentLayout.Exits = currentLayoutExits;
                        
                        layoutsAtThisDirection.Add(currentLayout);
                    }
                }

                return bakedRoom;
            }

            private Vector3Int TransformPoint(Vector3Int point, PatternMatrixSequence matrixSequence)
            {
                var patternTransformer = _owner._patternTransformController;
                var sequence = matrixSequence.Sequence;
                for (var i = 0; i < sequence.Count; i++)
                {
                    point = patternTransformer.TransformDirection(point, sequence[i]);
                }

                return point;
            }

            public void Dispose(BakedRoom bakedRoom)
            {
                bakedRoom.Source = null;
                var roomLayouts = bakedRoom.Layouts;
                
                for (var i = 0; i < roomLayouts.Length; i++)
                {
                    var layouts = roomLayouts[i];
                    if (layouts == null)
                    {
                        continue;
                    }

                    for (var j = 0; j < layouts.Count; j++)
                    {
                        var layoutVariant = layouts[j];
                        
                        layoutVariant.Layout.Clear();
                        _cellListPool.Return(layoutVariant.Layout);
                        layoutVariant.Layout = null;
                        
                        layoutVariant.Exits.Clear();
                        _directionListPool.Return(layoutVariant.Exits);
                        layoutVariant.Exits = null;
                        
                        _roomLayoutPool.Return(layoutVariant);
                    }
                    
                    layouts.Clear();
                    roomLayouts[i] = null;
                    _roomLayoutListPool.Return(layouts);
                }
                
                _roomPool.Return(bakedRoom);
            }
        }
    }
}