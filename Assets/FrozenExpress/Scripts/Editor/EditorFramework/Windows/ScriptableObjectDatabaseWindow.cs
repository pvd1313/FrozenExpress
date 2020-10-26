using System.Collections;
using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public partial class ScriptableObjectDatabaseWindow : EditorWindow
    {
        private readonly List<SerializedProperty> _items;
        private readonly SelectableListView<SerializedProperty> _itemListView;
        private readonly RepaintEventsView _repaintEvents;
        private readonly HorizontalSeparatorView _horizontalSeparatorView;
        private readonly SerializedPropertyView _propertyView;
        private readonly DatabaseWindowHeaderView _headerView;
        private readonly TabsView _tabsView;

        private ScriptableObject _database;
        private SerializedObject _databaseSerialized;
        private Vector2 _itemPreviewScrollPos;
        private int _selectedItemIndex;

        public ScriptableObjectDatabaseWindow()
        {
            _tabsView = new TabsView();
            _headerView = new DatabaseWindowHeaderView(this);
            _propertyView = new SerializedPropertyView();
            _horizontalSeparatorView = new HorizontalSeparatorView();
            _items = new List<SerializedProperty>();
            _itemListView = new SelectableListView<SerializedProperty>(GetPropertyName, GUILayout.Height(200));
            _repaintEvents = new RepaintEventsView(this);
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
                
                var item = ((IList) databaseInfo.ListField.GetValue(_database))[_selectedItemIndex];
                _tabsView.OnGUI(item);
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