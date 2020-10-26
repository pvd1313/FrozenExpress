using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;
using UnityEditor;

namespace FrozenExpress.Scripts.Game.LevelEditor.Editor
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