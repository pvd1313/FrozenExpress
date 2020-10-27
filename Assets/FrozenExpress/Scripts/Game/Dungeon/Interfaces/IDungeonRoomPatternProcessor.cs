using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Interfaces
{
    public interface IDungeonRoomPatternProcessor
    {
        void FindConnectionPoints(IReadOnlyDictionary<Vector3Int, int> room, List<PointDirection> result);
    }
}