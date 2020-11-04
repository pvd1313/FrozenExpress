using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using OneLine;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    [System.Serializable]
    public class RoomPattern
    {
        public string Id => _id;
        public IReadOnlyList<PatternMatrixSequence> Matrix => _matrix;
        public IReadOnlyList<CellLayer> Cells => _cells;

        public string ConnectorType => _connectorType;

        public bool IsEnabled => _isEnabled;

        [SerializeField] private string _id;
        
        [OneLine]
        [SerializeField] private List<CellLayer> _cells;
        
        [OneLine]
        [SerializeField] private List<PatternMatrixSequence> _matrix;

        [SerializeField] private bool _isEnabled;

        [SerializeField] private string _connectorType;
        
#if UNITY_EDITOR
        public static class EditorOnly
        {
            public static void OverrideCells(RoomPattern target, IReadOnlyDictionary<Vector3Int, int> layout)
            {
                target._cells.Clear();
                foreach (var item in  layout)
                {
                    CellLayer cellLayer;
                    cellLayer.Cell = item.Key;
                    cellLayer.Layer = item.Value;
                    target._cells.Add(cellLayer);
                }
            }
        }
#endif   
    }
}