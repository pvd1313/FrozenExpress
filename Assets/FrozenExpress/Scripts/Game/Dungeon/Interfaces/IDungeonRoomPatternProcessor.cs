using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonRoomPatternProcessor
    {
        void FindConnectionPoints(IReadOnlyDictionary<Vector3Int, int> room, List<PointDirection> result);
        bool CheckAllCellsConnected(IReadOnlyDictionary<Vector3Int, int> room);
    }
}