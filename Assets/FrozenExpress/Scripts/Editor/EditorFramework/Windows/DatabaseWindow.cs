using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    public partial class DatabaseWindow : EditorWindow
    {
        private readonly List<SerializedProperty> _items;
        private readonly SelectableListView<SerializedProperty> _itemListView;
        private readonly RepaintEventsView _repaintEvents;
        private readonly HorizontalSeparatorView _horizontalSeparatorView;
        private readonly SerializedPropertyView _propertyView;
        private readonly DatabaseWindowHeaderView _headerView;
        private readonly RuntimeTabsView _runtimeTabsView;
        private readonly StaticTabsView _staticTabsView;

        private ScriptableObject _database;
        private SerializedObject _databaseSerialized;
        private Vector2 _itemPreviewScrollPos;
        private int _selectedItemIndex;

        public DatabaseWindow()
        {
            _staticTabsView = new StaticTabsView();
            _runtimeTabsView = new RuntimeTabsView();
            _headerView = new DatabaseWindowHeaderView(this);
            _propertyView = new SerializedPropertyView();
            _horizontalSeparatorView = new HorizontalSeparatorView();
            _items = new List<SerializedProperty>();
            _itemListView = new SelectableListView<SerializedProperty>(GetPropertyName, GUILayout.Height(200));
            _repaintEvents = new RepaintEventsView(this);
        }
        
        private void OnEnable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (_database == null)
            {
                return;
            }
            
            var databaseInfo = DatabaseScanner.GetInfo(_database);
            if (!databaseInfo.IsDatabase)
            {
                return;
            }

            if (_databaseSerialized == null || _databaseSerialized.targetObject != _database)
            {
                return;
            }

            if (_selectedItemIndex < 0)
            {
                return;
            }
            
            var item = ((IList) databaseInfo.ListField.GetValue(_database))[_selectedItemIndex];
            Undo.RecordObject(_database, "Tab view");
            _staticTabsView.OnSceneGUI(sceneView, item);
            _runtimeTabsView.OnSceneGUI(sceneView, item);
        }

        private void OnGUI()
        {
            _repaintEvents.OnGUI();

            var headerAction = _headerView.OnGUI();

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

            _itemPreviewScrollPos = EditorGUILayout.BeginScrollView(_itemPreviewScrollPos);
            {
                var selectedItem = databaseList.GetArrayElementAtIndex(_selectedItemIndex);
                _propertyView.OnGUI(selectedItem);
                
                Undo.RecordObject(_database, "Tab view");
                var item = ((IList) databaseInfo.ListField.GetValue(_database))[_selectedItemIndex];
                _staticTabsView.OnGUI(item);
                _runtimeTabsView.OnGUI(item);
            }

            EditorGUILayout.EndScrollView();

            _headerView.HandleAction(headerAction);
        }

        private static string GetPropertyName(SerializedProperty property)
        {
            return property.displayName;
        }
    }
}