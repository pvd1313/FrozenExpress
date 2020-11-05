using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    public struct DungeonPreset
    {
        public int Seed;
        public int CellCount;
        public PointDirection EntryPoint;
        public string EntryPointConnector;
        public IReadOnlyList<CellLayer> InitialPoints;
        public BoundsInt Bounds;
    }
}