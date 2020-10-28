using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonMap : IDungeonMap
    {
        public IEnumerable<Vector3Int> Cells => _cells.Keys;

        private readonly Dictionary<Vector3Int, int> _cells;

        public DungeonMap()
        {
            _cells = new Dictionary<Vector3Int, int>();
        }

        public void Initialize(IReadOnlyDictionary<Vector3Int, int> dungeonLayout)
        {
            _cells.Clear();

            foreach (var item in dungeonLayout)
            {
                _cells[item.Key] = item.Value;
            }
        }
        
        public int GetCellLayer(Vector3Int position)
        {
            _cells.TryGetValue(position, out var layer);
            return layer;
        }
    }
}
