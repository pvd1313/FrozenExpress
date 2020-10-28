using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public struct CellLayer
    {
        public Vector3Int Cell;
        public int Layer;
    }
}