using System;
using System.Collections;
using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;
using EditorGUI = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUI;
using Object = UnityEngine.Object;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public class ScriptableObjectDatabaseWindow : EditorWindow
    {
        private readonly List<SerializedProperty> _items;
        private readonly SelectableListView<SerializedProperty> _listView;
        private readonly RepaintEventsView _repaintEvents;
        
        private ScriptableObject _database;
        private SerializedObject _databaseSerialized;
        private Vector2 _scrollPosition;
        private int _selectedIndex;

        public ScriptableObjectDatabaseWindow()
        {
            _items = new List<SerializedProperty>();
            _listView = new SelectableListView<SerializedProperty>(GetPropertyName, GUILayout.Height(300));
            _repaintEvents = new RepaintEventsView(this);
        }

        protected void OnGUI()
        {
            _repaintEvents.OnGUI();
            
            _database = EditorGUILayout.ObjectField("Database", _database, 
                typeof(ScriptableObject), false) as ScriptableObject;
            
            if (_database == null)
            {
                return;
            }

            var databaseInfo = DatabaseScanner.GetInfo(_database);
            if (string.IsNullOrWhiteSpace(databaseInfo.SerializedPropertyName))
            {
                EditorGUILayout.HelpBox("Selected asset is not valid database.", MessageType.Error);
                return;
            }

            if (_databaseSerialized == null || _databaseSerialized.targetObject != _database)
            {
                _databaseSerialized = new SerializedObject(_database);
            }

            var listProperty = _databaseSerialized.FindProperty(databaseInfo.SerializedPropertyName);
            var listSize = listProperty.arraySize;
            _items.Clear();
            for (var i = 0; i < listSize; i++)
            {
                _items.Add(listProperty.GetArrayElementAtIndex(i));
            }

            _selectedIndex = _listView.OnGUI(_selectedIndex, _items);
            if (_selectedIndex < 0)
            {
                return;    
            }

            var selectedItem = listProperty.GetArrayElementAtIndex(_selectedIndex);
            
            var wasExpanded = selectedItem.isExpanded;
            selectedItem.isExpanded = true;
            
            UnityEditor.EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(selectedItem);
            var anyChanges = UnityEditor.EditorGUI.EndChangeCheck();
            
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