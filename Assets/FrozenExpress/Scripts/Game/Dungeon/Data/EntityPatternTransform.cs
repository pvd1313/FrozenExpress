using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public struct EntityPatternTransform
    {
        public DungeonEntity Entity;
        public IReadOnlyList<EPatternTransform> Transform;
        public Vector3Int Position;
    }
}