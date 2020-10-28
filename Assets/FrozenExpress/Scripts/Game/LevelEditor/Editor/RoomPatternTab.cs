using System;
using System.Collections.Generic;
using FrozenExpress.Editor.EditorFramework;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Dungeon;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Game.LevelEditor.Editor
{
    public class RoomPatternTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(RoomPattern);
        
        private readonly ILevelEditorModel _levelModel;
        private readonly IDungeonRoomPatternProcessor _roomPatternProcessor;
        private readonly IDungeonTransform _dungeonTransform;
        private readonly ADebugDrawer _connectionsDrawer;
        private readonly ADebugLogger _logger;

        private readonly List<PointDirection> _connectionPoints;

        public RoomPatternTab(ILevelEditorModel levelModel,
                              IDungeonRoomPatternProcessor roomPatternProcessor,
                              ADebugFactory debugFactory,
                              IDungeonTransform dungeonTransform)
        {
            _levelModel = levelModel;
            _roomPatternProcessor = roomPatternProcessor;
            _dungeonTransform = dungeonTransform;
            _connectionPoints = new List<PointDirection>();
            
            _connectionsDrawer = debugFactory.CreateDrawer(this);
            _connectionsDrawer?.SetShape(EDebugShape.Arrow3D);
            _connectionsDrawer?.SetColor(EDebugColor.Yellow);
            _connectionsDrawer?.SetScale(Vector3.one * 2);
            _connectionsDrawer?.SetDuration(2);

            _logger = debugFactory.CreateLogger(this);
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

                if (GUILayout.Button("Detect connections"))
                {
                    _roomPatternProcessor.FindConnectionPoints(_levelModel.Layout, _connectionPoints);
                    foreach (var connection in _connectionPoints)
                    {
                        var pos = connection.Point;
                        var viewPos = _dungeonTransform.TransformPosition(pos);
                        var viewRot = Quaternion.LookRotation(connection.Direction);
                        _connectionsDrawer.Draw(viewPos, viewRot);
                    }
                    
                    _logger.Info($"Found {_connectionPoints.Count} points");
                }

                if (GUILayout.Button("Check shape"))
                {
                    var cellsConnected = _roomPatternProcessor.CheckAllCellsConnected(_levelModel.Layout);
                    if (cellsConnected)
                    {
                        _logger?.Info("All cells connected");
                    }
                    else
                    {
                        _logger?.Error("Some cells are not connected");
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}