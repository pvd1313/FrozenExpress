using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using UnityEngine;
using GreatRandom = UnityEngine.Random;

namespace FrozenExpress.Game.DungeonGenerator
{
    partial class DungeonLayoutGenerator
    {
        private class RoomPlacer
        {
            private readonly DungeonLayoutGenerator _owner;
            private readonly List<RoomLayout> _layoutsBuffer;
            private readonly List<CellLayer> _layerBuffer;
            
            public RoomPlacer(DungeonLayoutGenerator owner)
            {
                _owner = owner;
                _layoutsBuffer = new List<RoomLayout>();
                _layerBuffer = new List<CellLayer>();
            }

            public bool TryPlaceRoom(BakedRoom room, PointDirection worldExit)
            {
                var worldMap = _owner._worldMap;
                var worldBounds = _owner._worldBounds;
                var worldExits = _owner._worldExits;
                var worldExitConnectors = _owner._worldExitConnectors;
                
                var directionIndex = DirectionToIndex(worldExit.Direction * -1);
                var layouts = room.Layouts[directionIndex];
                if (layouts == null)
                {
                    return false;
                }

                _layoutsBuffer.Clear();
                _layoutsBuffer.AddRange(layouts);
                while (_layoutsBuffer.Count > 0)
                {
                    var layoutIndex = GreatRandom.Range(0, _layoutsBuffer.Count);
                    var layout = _layoutsBuffer[layoutIndex];
                    _layoutsBuffer.RemoveAt(layoutIndex);

                    var boundsConflict = false;
                    var boundsMap = layout.Bounds;
                    for (var i = 1; i < boundsMap.Count; i++)
                    {
                        var point = boundsMap[i] + worldExit.Point;
                        if (!worldBounds.Contains(point) || worldMap.ContainsKey(point))
                        {
                            boundsConflict = true;
                            break;
                        }
                    }
                    if (boundsConflict)
                    {
                        continue;
                    }
                    
                    _layerBuffer.Clear();
                    var layoutMap = layout.Layout;
                    _layerBuffer.Add(layoutMap[0]);
                    for (var i = 1; i < layoutMap.Count; i++)
                    {
                        var cellLayer = layoutMap[i];
                        var point = worldExit.Point + cellLayer.Cell;
                        if (!worldBounds.Contains(point) || worldMap.ContainsKey(point))
                        {
                            break;
                        }
                        
                        cellLayer.Cell = point;
                        _layerBuffer.Add(cellLayer);
                    }

                    if (layoutMap.Count == _layerBuffer.Count)
                    {
                        worldMap[worldExit.Point] |= _layerBuffer[0].Layer;
                        for (var i = 1; i < _layerBuffer.Count; i++)
                        {
                            var cellLayer = _layerBuffer[i];
                            worldMap[cellLayer.Cell] = cellLayer.Layer;
                        }

                        var layoutExits = layout.Exits;
                        for (var i = 0; i < layoutExits.Count; i++)
                        {
                            var exit = layoutExits[i];
                            exit.Point = worldExit.Point + exit.Point;
                            worldExits.Add(exit);
                            worldExitConnectors.Add(exit.Point, room.Source.ConnectorType);
                        }

                        return true;
                    }
                }

                return false;
            }
        }
    }
}