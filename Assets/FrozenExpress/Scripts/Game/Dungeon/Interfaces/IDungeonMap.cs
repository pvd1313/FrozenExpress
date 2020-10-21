using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonMap
    {
        IEnumerable<Vector3Int> Cells { get; }

        bool CheckCellExists(Vector3Int position);
    }
}
