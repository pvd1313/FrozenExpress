using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public interface IDungeonRoomPatternProcessor
    {
        void FindConnectionPoints(IReadOnlyDictionary<Vector3Int, int> room, List<PointDirection> result);
        bool CheckAllCellsConnected(IReadOnlyDictionary<Vector3Int, int> room);
    }
}