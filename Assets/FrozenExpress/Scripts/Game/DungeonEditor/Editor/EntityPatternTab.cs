using System;
using FrozenExpress.Editor.EditorFramework;
using FrozenExpress.Game.Dungeon;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor.Editor
{
    public class EntityPatternTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(EntityPattern);
        
        private readonly IDungeonEditorModel _dungeonModel;

        public EntityPatternTab(IDungeonEditorModel dungeonModel)
        {
            _dungeonModel = dungeonModel;
        }
        
        public void OnGUI(object data)
        {
            if (data is EntityPattern pattern)
            {
                EditorGUILayout.LabelField("Hello, from tab!");
            }
        }

        public void OnSceneGUI(Camera camera, object data)
        {
            
        }
    }
}