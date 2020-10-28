using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonMap
    {
        IEnumerable<Vector3Int> Cells { get; }

        int GetCellLayer(Vector3Int position);
    }
}
