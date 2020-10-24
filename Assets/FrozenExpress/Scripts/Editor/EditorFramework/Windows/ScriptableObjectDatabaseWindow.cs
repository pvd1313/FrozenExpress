using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.EditorFramework.Data;
using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public class ScriptableObjectDatabaseWindow : EditorWindow
    {
        private readonly List<SerializedProperty> _items;
        private readonly SelectableListView<SerializedProperty> _itemListView;
        private readonly RepaintEventsView _repaintEvents;

        private ScriptableObject _database;
        private SerializedObject _databaseSerialized;
        private int _selectedItemIndex;
        private int _selectedTabIndex;
        private Vector2 _itemRawScrollPos;

        public ScriptableObjectDatabaseWindow()
        {
            _items = new List<SerializedProperty>();
            _itemListView = new SelectableListView<SerializedProperty>(GetPropertyName, GUILayout.Height(200));
            _repaintEvents = new RepaintEventsView(this);
        }

        private void OnGUI()
        {
            _repaintEvents.OnGUI();
            
            _database = EditorGUILayout.ObjectField("Database", _database, 
                typeof(ScriptableObject), false) as ScriptableObject;
            
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
            }

            var listProperty = _databaseSerialized.FindProperty(databaseInfo.ListName);
            var listSize = listProperty.arraySize;
            _items.Clear();
            for (var i = 0; i < listSize; i++)
            {
                _items.Add(listProperty.GetArrayElementAtIndex(i));
            }

            _selectedItemIndex = _itemListView.OnGUI(_selectedItemIndex, _items);
            if (_selectedItemIndex < 0)
            {
                return;    
            }
            
            var separatorStyle = new GUIStyle(GUIStylesConfig.HorizontalSeparator);
            EditorGUILayout.LabelField("", separatorStyle);
            
            _databaseSerialized.Update();
            var selectedItem = listProperty.GetArrayElementAtIndex(_selectedItemIndex);
            
            var wasExpanded = selectedItem.isExpanded;
            selectedItem.isExpanded = true;

            EditorGUI.BeginChangeCheck();
            _itemRawScrollPos = EditorGUILayout.BeginScrollView(_itemRawScrollPos/*, GUILayout.Height(300)*/);
            EditorGUILayout.PropertyField(selectedItem);
            EditorGUILayout.EndScrollView();
            var anyChanges = EditorGUI.EndChangeCheck();
            
            if (anyChanges)
            {
                _databaseSerialized.ApplyModifiedProperties();
            }

            selectedItem.isExpanded = wasExpanded;
        }

        private static string GetPropertyName(SerializedProperty property)
        {
            return property.displayName;
        }
    }
}