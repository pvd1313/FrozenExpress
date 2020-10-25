using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Models
{
    public class LevelEditorModel : ILevelEditorModel
    {
        public event Action OnLayoutChanged;
        public Vector3Int CursorPosition { get; set; }
        public IReadOnlyDictionary<Vector3Int, int> Layout =>  _cells;

        private readonly Dictionary<Vector3Int, int> _cells;

        public LevelEditorModel()
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
    }
}
