using System;
using System.Linq;
using FrozenExpress.Editor.EditorFramework;
using FrozenExpress.Game.Dungeon;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FrozenExpress.Scripts.Game.Dungeon.Editor
{
    [DatabaseWindowTab(typeof(DungeonEntity))]
    public class DungeonEntityTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(DungeonEntity);

        private readonly Matrix4x4 _matrixIdentity;
        
        private DungeonEntityEditorConfig _config;

        public DungeonEntityTab()
        {
            _matrixIdentity = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }
        
        public void OnGUI(object data)
        {
            _config = FrozenEditorGUILayout.AssetField("Config", _config);
            
            if (_config == null)
            {
                _config = FindAsset<DungeonEntityEditorConfig>();
            }
        }

        public void OnSceneGUI(SceneView sceneView, object data)
        {
            if (Event.current.type != EventType.Repaint)
            {
                return;
            }
            
            if (_config == null || !(data is DungeonEntity entity))
            {
                return;
            }
            
            var camera = sceneView.camera;

            Handles.color = _config.RoomBoundsColor;
            Handles.DrawWireCube(Vector3.zero, _config.RoomSize);
            Handles.PositionHandle(Vector3.zero, Quaternion.identity);

            if (_config.ColliderMaterial != null && _config.CubeMesh != null)
            {
                foreach (var bounds in entity.Colliders)
                {
                    var rotation = Quaternion.Euler(bounds.Rotation);
                    var matrix = Matrix4x4.TRS(bounds.Position, rotation, bounds.Size);
                    Graphics.DrawMesh(_config.CubeMesh, matrix, _config.ColliderMaterial, 1, camera);
                }
            }

            if (_config.EntityMaterial != null && entity.Mesh != null)
            {
                Graphics.DrawMesh(entity.Mesh, _matrixIdentity, _config.EntityMaterial, 1, camera);
            }
        }

        private static T FindAsset<T>() where T : Object
        {
            var filter = "t:" + typeof(T).Name;
            var guids = AssetDatabase.FindAssets(filter);
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null)
                {
                    return asset;
                }
            }

            return null;
        }
    }
}