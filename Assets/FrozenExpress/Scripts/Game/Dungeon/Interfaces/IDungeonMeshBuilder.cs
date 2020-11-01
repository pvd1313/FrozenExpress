using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    public interface IDungeonMeshBuilder
    {
        void BuildMesh(IEnumerable<EntityPatternTransform> entityTransforms, Mesh result);
    }
}
