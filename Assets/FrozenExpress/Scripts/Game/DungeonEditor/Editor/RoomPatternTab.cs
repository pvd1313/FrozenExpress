using System;
using System.Collections.Generic;
using FrozenExpress.Editor.EditorFramework;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Dungeon;
using FrozenExpress.Game.DungeonGenerator;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor.Editor
{
    public class RoomPatternTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(RoomPattern);
        
        private readonly IDungeonEditorModel _dungeonModel;
        private readonly IDungeonRoomPatternProcessor _roomPatternProcessor;
        private readonly IDungeonTransform _dungeonTransform;
        private readonly IDungeonLayoutGenerator _dungeonLayoutGenerator;
        private readonly ADebugDrawer _connectionsDrawer;
        private readonly ADebugLogger _logger;

        private readonly List<PointDirection> _connectionPoints;
        private readonly Dictionary<Vector3Int, int> _dungeonLayout;
        private readonly List<CellLayer> _dungeonInitialPoints;

        private BoundsInt _dungeonBounds;
        private int _dungeonSize;
        private int _dungeonSeed;
        private bool _dungeonSeedRandom;

        public RoomPatternTab(IDungeonEditorModel dungeonModel,
                              IDungeonRoomPatternProcessor roomPatternProcessor,
                              ADebugFactory debugFactory,
                              IDungeonTransform dungeonTransform,
                              IDungeonLayoutGenerator dungeonLayoutGenerator)
        {
            _dungeonModel = dungeonModel;
            _roomPatternProcessor = roomPatternProcessor;
            _dungeonTransform = dungeonTransform;
            _dungeonLayoutGenerator = dungeonLayoutGenerator;
            _connectionPoints = new List<PointDirection>();
            _dungeonLayout = new Dictionary<Vector3Int, int>();
            _dungeonSeedRandom = true;
            _dungeonSize = 100;
            _dungeonBounds.min = new Vector3Int(-100, -100, 0);
            _dungeonBounds.max = new Vector3Int(100, 100, 100);
            
            _dungeonInitialPoints = new List<CellLayer>();
            for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                CellLayer cellLayer;
                cellLayer.Cell = new Vector3Int(x, y, -1);
                cellLayer.Layer = 1;
                _dungeonInitialPoints.Add(cellLayer);
            }

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
                    _dungeonModel.Clear();
                    
                    var cells = pattern.Cells;
                    for (var i = 0; i < cells.Count; i++)
                    {
                        var cellLayer = cells[i];
                        _dungeonModel.SetCellLayer(cellLayer.Cell, cellLayer.Layer);
                    }
                }

                if (GUILayout.Button("Import from game"))
                {
                    RoomPattern.EditorOnly.OverrideCells(pattern, _dungeonModel.Layout);
                }

                if (GUILayout.Button("Detect connections"))
                {
                    _roomPatternProcessor.FindConnectionPoints(_dungeonModel.Layout, _connectionPoints);
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
                    var cellsConnected = _roomPatternProcessor.CheckAllCellsConnected(_dungeonModel.Layout);
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
            
            _dungeonSize = EditorGUILayout.IntField("Dungeon size", _dungeonSize);
            _dungeonSeed = EditorGUILayout.IntField("Dungeon seed", _dungeonSeed);
            _dungeonSeedRandom = EditorGUILayout.ToggleLeft("Random seed", _dungeonSeedRandom);
            if (GUILayout.Button("Generate!"))
            {
                if (_dungeonSeedRandom)
                {
                    _dungeonSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                }

                DungeonPreset dungeonPreset;
                dungeonPreset.Seed = _dungeonSeed;
                dungeonPreset.CellCount = _dungeonSize;
                dungeonPreset.EntryPoint.Point = Vector3Int.zero;
                dungeonPreset.EntryPoint.Direction = new Vector3Int(0, 0, 1);
                dungeonPreset.EntryPointConnector = "room";
                dungeonPreset.InitialPoints = _dungeonInitialPoints;
                dungeonPreset.Bounds = _dungeonBounds;

                _dungeonLayoutGenerator.GenerateDungeon(dungeonPreset, _dungeonLayout);
                _dungeonModel.Clear();
                foreach (var cell in _dungeonLayout)
                {
                    _dungeonModel.SetCellLayer(cell.Key, cell.Value);
                }
            }
        }

        public void OnSceneGUI(Camera camera, object data)
        {
            
        }
    }
}