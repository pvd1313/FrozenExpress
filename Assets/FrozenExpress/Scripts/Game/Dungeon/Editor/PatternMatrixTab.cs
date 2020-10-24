using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Editor
{
    public class PatternMatrixTab : IDatabaseTab
    {
        public Type TargetType => typeof(Pattern);

        public void OnGUI(object data)
        {
            if (data is Pattern pattern)
            {
                EditorGUILayout.TextField("Name?.lol)))", pattern.Name);
            }
        }

        public void OnRender(Camera cameraLocalToWorld)
        {
            
        }
    }
}