using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Data
{
    [System.Serializable]
    public struct CellLayer
    {
        public Vector3Int Cell;
        public int Layer;
    }
}