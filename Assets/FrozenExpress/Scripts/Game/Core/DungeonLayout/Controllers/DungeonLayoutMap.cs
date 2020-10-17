using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutMap : IDungeonLayoutMap
    {
        public Vector3Int Forward { get; }
        public Vector3Int Backward { get; }
        public Vector3Int Right { get; }
        public Vector3Int Left { get; }

        public IEnumerable<Vector3Int> Cells => _cells;

        private readonly HashSet<Vector3Int> _cells;

        public DungeonLayoutMap()
        {
            Forward = new Vector3Int(0, 0, 1);
            Backward = new Vector3Int(0, 0, -1);
            Right = new Vector3Int(1, 0, 0);
            Left = new Vector3Int(-1, 0, 0);

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
