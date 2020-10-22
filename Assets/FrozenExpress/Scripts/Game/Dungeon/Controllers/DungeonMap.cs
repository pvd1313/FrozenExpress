using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.Dungeon.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Controllers
{
    public class DungeonMap : IDungeonMap
    {
        public IEnumerable<Vector3Int> Cells => _cells;

        private readonly HashSet<Vector3Int> _cells;

        public DungeonMap()
        {
            _cells = new HashSet<Vector3Int>();
        }

        public void Initialize(DungeonLayout dungeonLayout)
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
