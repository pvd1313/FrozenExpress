using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor.Models
{
    public class DungeonEditorModel : IDungeonEditorModel
    {
        public event Action OnLayoutChanged;
        public Vector3Int CursorPosition { get; set; }
        public IReadOnlyDictionary<Vector3Int, int> Layout =>  _cells;

        private readonly Dictionary<Vector3Int, int> _cells;

        public DungeonEditorModel()
        {
            _cells = new Dictionary<Vector3Int, int>();
        }

        public int GetCellLayer(Vector3Int cell)
        {
            _cells.TryGetValue(cell, out var layer);
            return layer;
        }

        public void SetCellLayer(Vector3Int cell, int layer)
        {
            if (layer == 0)
            {
                if (_cells.Remove(cell))
                {
                    OnLayoutChanged?.Invoke();
                }
                
                return;
            }

            _cells[cell] = layer;
            OnLayoutChanged?.Invoke();
        }

        public void Clear()
        {
            var count = _cells.Count;
            
            _cells.Clear();
            
            if (count > 0)
            {
                OnLayoutChanged?.Invoke();
            }
        }
    }
}
