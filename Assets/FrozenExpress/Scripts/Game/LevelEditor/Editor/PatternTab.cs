using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;
using UnityEditor;

namespace FrozenExpress.Scripts.Game.LevelEditor.Editor
{
    public class PatternTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(Pattern);
        
        private readonly ILevelEditorModel _levelModel;

        public PatternTab(ILevelEditorModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        public void OnGUI(object data)
        {
            if (data is Pattern pattern)
            {
                EditorGUILayout.LabelField("Hello, from tab!");
            }
        }
    }
}