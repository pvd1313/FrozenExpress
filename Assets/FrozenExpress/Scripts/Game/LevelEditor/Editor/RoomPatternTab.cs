using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Editor
{
    public class RoomPatternTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(RoomPattern);
        
        private readonly ILevelEditorModel _levelModel;

        public RoomPatternTab(ILevelEditorModel levelModel)
        {
            _levelModel = levelModel;
        }
        public void OnGUI(object data)
        {
            if (!(data is RoomPattern pattern))
            {
                return;
            };
            
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Export to game"))
                {
                    _levelModel.Clear();
                    
                    var cells = pattern.Cells;
                    for (var i = 0; i < cells.Count; i++)
                    {
                        var cellLayer = cells[i];
                        _levelModel.SetCellLayer(cellLayer.Cell, cellLayer.Layer);
                    }
                }

                if (GUILayout.Button("Import from game"))
                {
                    RoomPattern.EditorOnly.OverrideCells(pattern, _levelModel.Layout);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}