using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.EditorFramework.Data;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Views
{
    public class TabListView<T>
    {
        private readonly Func<T, string> _getName;

        private bool _initialized;
        private GUIStyle _tabStyleNormal;
        private GUIStyle _tabStyleSelected;
        
        public TabListView(Func<T, string> getName)
        {
            _getName = getName;
        }

        public int OnGUI(int selectedIndex, List<T> tabs)
        {
            Initialize();

            EditorGUILayout.BeginHorizontal();
            for (var i = 0; i < tabs.Count; i++)
            {
                var style = i == selectedIndex ? _tabStyleSelected : _tabStyleNormal;
                if (GUILayout.Button(_getName(tabs[i]), style))
                {
                    selectedIndex = i;
                }
            }
            EditorGUILayout.EndHorizontal();
            
            return Mathf.Min(selectedIndex, tabs.Count - 1);
        }

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            _tabStyleNormal = new GUIStyle(GUIStylesConfig.ListItemNormal);
            _tabStyleSelected = new GUIStyle(GUIStylesConfig.ListItemSelected);
        }
    }
}