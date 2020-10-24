using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEditor;

namespace FrozenExpress.Scripts.Game.Dungeon.Editor
{
    public class PatternMatrixTab : IDatabaseTab
    {
        public Type TargetType => typeof(Pattern);

        public void OnGadgetGUI(object data)
        {
            if (data is Pattern pattern)
            {
                EditorGUILayout.TextField("Name?.lol)))", pattern.Name);
            }
        }
    }
}