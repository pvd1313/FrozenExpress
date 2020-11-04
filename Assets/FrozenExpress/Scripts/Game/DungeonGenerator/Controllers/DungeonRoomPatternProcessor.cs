using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public class DungeonRoomPatternProcessor : IDungeonRoomPatternProcessor
    {
        private readonly Vector3Int[] _siblingDirections;
        private readonly HashSet<Vector3Int> _visitBuffer;
        private readonly List<PointDirection> _deadPointBuffer;

        public DungeonRoomPatternProcessor()
        {
            _visitBuffer = new HashSet<Vector3Int>();
            _deadPointBuffer = new List<PointDirection>();
            _siblingDirections = new[]
            {
                new Vector3Int(0, 0, 1),
                new Vector3Int(0, 0, -1),
                new Vector3Int(0, 1, 0),
                new Vector3Int(0, -1, 0),
                new Vector3Int(1, 0, 0),
                new Vector3Int(-1, 0, 0),
            };
        }
        
        public void FindConnectionPoints(IReadOnlyDictionary<Vector3Int, int> room, List<PointDirection> result)
        {
            result.Clear();
            
            foreach (var cell in room.Keys)
            {
                if (CheckValidConnectionPoint(room, cell, out var direction))
                {
                    PointDirection pointDirection;
                    pointDirection.Point = cell;
                    pointDirection.Direction = direction;
                    
                    result.Add(pointDirection);
                }
            }
        }

        private bool CheckValidConnectionPoint(IReadOnlyDictionary<Vector3Int, int> room, Vector3Int point, out Vector3Int direction)
        {
            direction = default;
            
            var siblingCount = 0;
            
            for (var i = 0; i < _siblingDirections.Length && siblingCount < 2; i++)
            {
                var siblingDirection = _siblingDirections[i];
                
                if (room.ContainsKey(point + siblingDirection))
                {
                    direction = -siblingDirection;
                    
                    siblingCount++;
                }
            }

            return siblingCount == 1;
        }

        public void RemoveDeadEnds(IDictionary<Vector3Int, int> map)
        {
            var mapReadOnly = (IReadOnlyDictionary<Vector3Int, int>) map;

            FindConnectionPoints(mapReadOnly, _deadPointBuffer);
            
            while (_deadPointBuffer.Count > 0)
            {
                for (var i = 0; i < _deadPointBuffer.Count; i++)
                {
                    RemoveDeadPoint(map, _deadPointBuffer[i].Point);
                }
                
                FindConnectionPoints(mapReadOnly, _deadPointBuffer);
            }
        }

        private void RemoveDeadPoint(IDictionary<Vector3Int, int> map, Vector3Int deadPoint)
        {
            if (!map.ContainsKey(deadPoint))
            {
                return;
            }
            
            while (true)
            {
                var siblingCount = 0;
                Vector3Int siblingPoint = default;
                for (var i = 0; i < _siblingDirections.Length && siblingCount < 2; i++)
                {
                    var testPoint = deadPoint + _siblingDirections[i];

                    if (map.ContainsKey(testPoint))
                    {
                        siblingPoint = testPoint;
                        siblingCount++;
                    }
                }

                if (siblingCount > 1)
                {
                    return;
                }

                map.Remove(deadPoint);

                if (siblingCount == 1)
                {
                    deadPoint = siblingPoint;
                    continue;
                }

                break;
            }
        }

        public bool CheckAllCellsConnected(IReadOnlyDictionary<Vector3Int, int> room)
        {
            if (room.Count == 0)
            {
                return true;
            }
            
            _visitBuffer.Clear();
            
            VisitRecursive(room, room.Keys.First());
            
            return _visitBuffer.Count == room.Count;
        }

        public void FindBoundCells(IReadOnlyDictionary<Vector3Int, int> room, List<Vector3Int> result)
        {
            _visitBuffer.Clear();
            result.Clear();

            foreach (var index in room.Keys)
            {
                for (var i = 0; i < _siblingDirections.Length; i++)
                {
                    var position = index + _siblingDirections[i];
                    if (room.ContainsKey(position))
                    {
                        continue;
                    }

                    if (_visitBuffer.Add(position))
                    {
                        result.Add(position);
                    }
                }
            }
        }

        private void VisitRecursive(IReadOnlyDictionary<Vector3Int, int> room, Vector3Int point)
        {
            if (_visitBuffer.Contains(point) || !room.ContainsKey(point))
            {
                return;
            }

            _visitBuffer.Add(point);

            for (var i = 0; i < _siblingDirections.Length; i++)
            {
                var siblingDirection = _siblingDirections[i];
                
                VisitRecursive(room, point + siblingDirection);
            }
        }
        
        
    }
}