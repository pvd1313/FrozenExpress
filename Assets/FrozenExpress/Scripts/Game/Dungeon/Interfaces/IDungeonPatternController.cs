using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonPatternController
    {
        IEnumerable<MeshTransform> IterateEntities(IReadOnlyDictionary<Vector3Int, int> layoutMap);
    }
}
