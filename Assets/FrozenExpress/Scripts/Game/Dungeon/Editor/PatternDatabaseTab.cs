using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEditor;

namespace FrozenExpress.Scripts.Game.Dungeon.Editor
{
    public class PatternDatabaseTab : IDatabaseTab
    {
        public Type TargetType => typeof(Pattern);
        
        public void OnGUI(object data)
        {
            if (data is Pattern pattern)
            {
                EditorGUILayout.TextField("Hello, from tab!");
            }
        }
    }
}