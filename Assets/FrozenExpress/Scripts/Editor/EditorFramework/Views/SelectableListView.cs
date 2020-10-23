using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.GUIStyles;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Views
{
    public class SelectableListView<T>
    {
        private readonly Func<T, string> _getName;
        private readonly GUILayoutOption[] _scrollViewLayoutOptions;

        private bool _initialized;
        private GUIStyle _itemStyle;
        private GUIStyle _selectedItemStyle;
        
        private Vector2 _scrollPos;

        public SelectableListView(Func<T, string> getName, params GUILayoutOption[] scrollViewLayoutOptions)
        {
            _getName = getName;
            _scrollViewLayoutOptions = scrollViewLayoutOptions;
        }
        
        public int OnGUI(int selectedIndex, List<T> items)
        {
            Initialize();
            
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, _scrollViewLayoutOptions);
            {
                for (var i = 0; i < items.Count; i++)
                {
                    var style = i == selectedIndex ? _selectedItemStyle : _itemStyle;
                    if (GUILayout.Button(_getName(items[i]), style))
                    {
                        selectedIndex = i;
                    }
                }
            }

            EditorGUILayout.EndScrollView();

            return Mathf.Min(selectedIndex, items.Count - 1);;
        }

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            _itemStyle = new GUIStyle( GUIStylesConfig.ListItem);
            _selectedItemStyle = new GUIStyle(GUIStylesConfig.SelectedListItem);
        }
    }
}