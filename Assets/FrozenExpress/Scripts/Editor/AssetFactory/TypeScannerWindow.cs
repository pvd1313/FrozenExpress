using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace FrozenExpress.Scripts.Editor.AssetFactory
{
    public class TypeScannerWindow
    {
        private const string FilterFocusControlName = "FilterFocusControlName";
        
        private readonly Type _targetType;
        private readonly List<TypeInfo> _filteredTypes;
        private readonly GUIContent _searchIcon;

        private bool _focusedOnce;
        private string _filter;
        private Vector2 _scrollPos;

        public TypeScannerWindow(Type targetType)
        {
            _targetType = targetType;
            _filteredTypes = new List<TypeInfo>();
            _filter = string.Empty;
            _searchIcon = EditorGUIUtility.IconContent("d_Search Icon");
        }

        public bool OnGUI(out TypeInfo selectedType)
        {
            selectedType = default(TypeInfo);

            var searchTextStyle = new GUIStyle("SearchTextField");
            GUI.SetNextControlName(FilterFocusControlName);
            _filter = EditorGUILayout.TextField(_filter, searchTextStyle);

            TypeScanner.SearchTypes(_targetType, _filter, _filteredTypes);

            var selectedAny = false;
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandWidth(false));
            {
                var buttonStyle = GUI.skin.textField;
                foreach (var type in _filteredTypes)
                {
                    if (GUILayout.Button(type.NameShort, buttonStyle, GUILayout.Width(WindowUtil.WindowWidth - 19)))
                    {
                        selectedAny = true;
                        selectedType = type;
                    }
                }
            } 
            EditorGUILayout.EndScrollView();

            if (!_focusedOnce)
            {
                _focusedOnce = true;
                GUI.FocusControl(FilterFocusControlName);
            }
            
            return selectedAny;
        }
    }
}