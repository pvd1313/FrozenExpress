using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
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

            var curEvent = Event.current;
            if (curEvent != null && curEvent.type == EventType.KeyDown)
            {
                if (curEvent.keyCode == KeyCode.UpArrow && selectedIndex > 0)
                {
                    selectedIndex--;
                }

                if (curEvent.keyCode == KeyCode.DownArrow)
                {
                    selectedIndex++;
                }
            }

            selectedIndex = Mathf.Min(selectedIndex, items.Count - 1);
            
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

            return selectedIndex;
        }

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            _itemStyle = new GUIStyle( GUIStylesConfig.ListItemNormal);
            _selectedItemStyle = new GUIStyle(GUIStylesConfig.ListItemSelected);
        }
    }
}