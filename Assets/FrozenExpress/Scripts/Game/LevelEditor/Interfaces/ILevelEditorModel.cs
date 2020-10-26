using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Interfaces
{
    public interface ILevelEditorModel
    {
        event Action OnLayoutChanged;

        Vector3Int CursorPosition { get; set; }
        
        IReadOnlyDictionary<Vector3Int, int> Layout { get; }

        int GetCellLayer(Vector3Int cell);
        void SetCellLayer(Vector3Int cell, int layer);

        void Clear();
    }
}
