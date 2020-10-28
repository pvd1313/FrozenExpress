using System;
using FrozenExpress.Editor.EditorFramework;
using FrozenExpress.Game.Dungeon;
using UnityEditor;

namespace FrozenExpress.Game.LevelEditor.Editor
{
    public class EntityPatternTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(EntityPattern);
        
        private readonly ILevelEditorModel _levelModel;

        public EntityPatternTab(ILevelEditorModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        public void OnGUI(object data)
        {
            if (data is EntityPattern pattern)
            {
                EditorGUILayout.LabelField("Hello, from tab!");
            }
        }
    }
}