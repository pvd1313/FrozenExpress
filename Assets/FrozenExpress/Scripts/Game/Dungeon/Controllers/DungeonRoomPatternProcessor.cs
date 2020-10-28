using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.Dungeon.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Controllers
{
    public class DungeonRoomPatternProcessor : IDungeonRoomPatternProcessor
    {
        private readonly Vector3Int[] _siblingDirections;
        
        public DungeonRoomPatternProcessor()
        {
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
    }
}