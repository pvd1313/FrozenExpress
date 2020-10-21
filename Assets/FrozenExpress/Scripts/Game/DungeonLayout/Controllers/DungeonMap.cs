using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonMap : IDungeonMap
    {
        public IEnumerable<Vector3Int> Cells => _cells;

        private readonly HashSet<Vector3Int> _cells;

        public DungeonMap()
        {
            _cells = new HashSet<Vector3Int>();
        }

        public void Initialize(DungeonLayoutData dungeonLayout)
        {
            _cells.Clear();

            foreach (var item in dungeonLayout.Cells)
            {
                _cells.Add(item);
            }
        }

        public bool CheckCellExists(Vector3Int position)
        {
            return _cells.Contains(position);
        }
    }
}
