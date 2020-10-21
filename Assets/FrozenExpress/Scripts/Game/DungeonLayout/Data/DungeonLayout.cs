using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    [System.Serializable]
    public class DungeonLayout
    {
        public IReadOnlyList<Vector3Int> Cells => _cells;

        [SerializeField] protected List<Vector3Int> _cells;
    }
}
