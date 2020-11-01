using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonPatternController
    {
        void ProcessMap(IReadOnlyDictionary<Vector3Int, int> map, ICollection<EntityPatternTransform> result);
    }
}
