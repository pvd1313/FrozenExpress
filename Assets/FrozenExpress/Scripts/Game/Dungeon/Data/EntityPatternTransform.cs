using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public struct EntityPatternTransform
    {
        public string EntityId;
        public IReadOnlyList<EPatternTransform> Transform;
        public Vector3Int Position;
    }
}