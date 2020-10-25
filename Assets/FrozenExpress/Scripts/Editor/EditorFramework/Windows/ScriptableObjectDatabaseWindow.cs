using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public class ScriptableObjectDatabaseWindow : EditorWindow
    {
        private static readonly GUIContent HeaderButtonContent = new GUIContent();
        private static readonly GUILayoutOption HeaderButtonWidth = GUILayout.Width(24);
        
        private readonly List<SerializedProperty> _items;
        private readonly SelectableListView<SerializedProperty> _itemListView;
        private readonly RepaintEventsView _repaintEvents;
        private readonly HorizontalSeparatorView _horizontalSeparatorView;
        private readonly SerializedPropertyView _propertyView;

        private ScriptableObject _database;
        private SerializedObject _databaseSerialized;
        private int _selectedItemIndex;

        public ScriptableObjectDatabaseWindow()
        {
            _propertyView = new SerializedPropertyView();
            _horizontalSeparatorView = new HorizontalSeparatorView();
            _items = new List<SerializedProperty>();
            _itemListView = new SelectableListView<SerializedProperty>(GetPropertyName, GUILayout.Height(200));
            _repaintEvents = new RepaintEventsView(this);
        }

        private void OnGUI()
        {
            _repaintEvents.OnGUI();

            var headerAction = OnHeaderGUI();

            if (_database == null)
            {
                return;
            }

            var databaseInfo = DatabaseScanner.GetInfo(_database);
            if (!databaseInfo.IsDatabase)
            {
                EditorGUILayout.HelpBox("Selected asset is not database", MessageType.Error);
                return;
            }

            if (_databaseSerialized == null || _databaseSerialized.targetObject != _database)
            {
                _databaseSerialized = new SerializedObject(_database);
                _items.Clear();
            }

            var databaseList = _databaseSerialized.FindProperty(databaseInfo.ListName);
            var listSize = databaseList.arraySize;
            _items.Clear();
            for (var i = 0; i < listSize; i++)
            {
                _items.Add(databaseList.GetArrayElementAtIndex(i));
            }

            _selectedItemIndex = _itemListView.OnGUI(_selectedItemIndex, _items);
            if (_selectedItemIndex < 0)
            {
                return;    
            }
            
            _horizontalSeparatorView.OnGUI();
            
            var selectedItem = databaseList.GetArrayElementAtIndex(_selectedItemIndex);
            _propertyView.OnGUI(selectedItem);
            
            HandleHeaderAction(headerAction);
        }

        private int OnHeaderGUI()
        {
            var result = 0;

            EditorGUILayout.BeginHorizontal();
            {
                _database = AssetField("Database", _database);
                result |= OnHeaderButton("D", 8, "Duplicate selected item");
                result |= OnHeaderButton("▲", 4, "Move up selected item");
                result |= OnHeaderButton("▼", 2, "Move down selected item");
                result |= OnHeaderButton("X", 1, "Remove selected item");
            }
            EditorGUILayout.EndHorizontal();
            
            return result;
        }

        private void HandleHeaderAction(int action)
        {
            if (action == 0)
            {
                return;
            }
            
            var databaseInfo = DatabaseScanner.GetInfo(_database);
            var databaseList = _databaseSerialized.FindProperty(databaseInfo.ListName);
            var selectedItem = databaseList.GetArrayElementAtIndex(_selectedItemIndex);
            switch (action)
            {
                case 1:
                    selectedItem.DeleteCommand();
                    break;
                case 8:
                    selectedItem.DuplicateCommand();
                    break;
                case 4:
                    if (databaseList.MoveArrayElement(_selectedItemIndex, _selectedItemIndex - 1))
                    {
                        _selectedItemIndex--;
                    }
                    break;
                case 2:
                    if (databaseList.MoveArrayElement(_selectedItemIndex, _selectedItemIndex + 1))
                    {
                        _selectedItemIndex++;
                    }
                    break;
            }

            _databaseSerialized.ApplyModifiedProperties();
        }

        private static int OnHeaderButton(string icon, int flag, string tooltip)
        {
            HeaderButtonContent.text = icon;
            HeaderButtonContent.tooltip = tooltip;
            
            return GUILayout.Button(HeaderButtonContent, HeaderButtonWidth) ? flag : 0;
        }

        private static T AssetField<T>(string label, T asset) where T : Object
        {
            return EditorGUILayout.ObjectField(label, asset, typeof(T), false) as T;
        }

        private static string GetPropertyName(SerializedProperty property)
        {
            return property.displayName;
        }
    }
}