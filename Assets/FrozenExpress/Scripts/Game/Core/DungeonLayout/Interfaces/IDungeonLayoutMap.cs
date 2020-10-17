using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public interface IDungeonLayoutMap
    {
        Vector3Int Forward { get; }
        Vector3Int Backward { get; }
        Vector3Int Right { get; }
        Vector3Int Left { get; }

        IEnumerable<Vector3Int> Cells { get; }

        bool CheckCellExists(Vector3Int position);
    }
}
