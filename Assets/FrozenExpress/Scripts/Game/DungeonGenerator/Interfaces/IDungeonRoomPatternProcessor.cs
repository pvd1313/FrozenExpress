using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public interface IDungeonRoomPatternProcessor
    {
        void FindConnectionPoints(IReadOnlyDictionary<Vector3Int, int> room, List<PointDirection> result);
        bool CheckAllCellsConnected(IReadOnlyDictionary<Vector3Int, int> room);

        void FindBoundCells(IReadOnlyDictionary<Vector3Int, int> room, List<Vector3Int> result);
        void RemoveDeadEnds(IDictionary<Vector3Int, int> map);
    }
}