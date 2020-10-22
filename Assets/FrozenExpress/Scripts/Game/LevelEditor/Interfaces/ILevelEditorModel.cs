using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Interfaces
{
    public interface ILevelEditorModel
    {
        event Action OnLayoutChanged;

        Vector3Int CursorPosition { get; set; }
        IEnumerable<Vector3Int> Cells { get; }

        void AddCell(Vector3Int cellIndex);

        void RemoveCell(Vector3Int cellIndex);

        bool CheckCellExists(Vector3Int cellIndex);
    }
}
