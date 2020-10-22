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

        public IEnumerable<Vector3Int> Cells => _cells;

        private readonly HashSet<Vector3Int> _cells;

        public LevelEditorModel()
        {
            _cells = new HashSet<Vector3Int>();
        }

        public void AddCell(Vector3Int cellIndex)
        {
            if (_cells.Add(cellIndex))
            {
                OnLayoutChanged?.Invoke();
            }
        }

        public void RemoveCell(Vector3Int cellIndex)
        {
            if (_cells.Remove(cellIndex))
            {
                OnLayoutChanged?.Invoke();
            }
        }

        public bool CheckCellExists(Vector3Int cellIndex)
        {
            return _cells.Contains(cellIndex);
        }
    }
}
