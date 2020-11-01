using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using UnityEngine;

namespace FrozenExpress.Game.DungeonCollision
{
    public interface IDungeonCollisionController
    {
        void InitializeColliders(IEnumerable<EntityPatternTransform> entities);
    }
}