using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.ScriptableObjectCreation
{
    public class TypeScannerWindow
    {
        private readonly Type _targetType;
        private readonly List<TypeInfo> _filteredTypes;

        private bool _focusedOnce;
        private string _filter;
        private Vector2 _scrollPos;

        public TypeScannerWindow(Type targetType)
        {
            _targetType = targetType;
            _filteredTypes = new List<TypeInfo>();
            _filter = string.Empty;
        }

        public bool OnGUI(out TypeInfo selectedType)
        {
            selectedType = default(TypeInfo);
            
            GUI.SetNextControlName(nameof(_filter));
            _filter = EditorGUILayout.TextField("Search: ", _filter);
            
            TypeScanner.SearchTypes(_targetType, _filter, _filteredTypes);

            var selectedAny = false;
            
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            {
                foreach (var type in _filteredTypes)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("+", GUILayout.Width(20)))
                        {
                            selectedAny = true;
                            selectedType = type;
                        }

                        EditorGUILayout.LabelField(type.NameShort);
                    }
                    GUILayout.EndHorizontal();
                }
            } 
            EditorGUILayout.EndScrollView();

            return selectedAny;
        }
    }
}