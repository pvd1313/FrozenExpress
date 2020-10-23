using System.Collections.Generic;
using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public class DatabaseWindow : EditorWindow
    {
        private readonly List<object> _items;
        private readonly SelectableListView<object> _listView;
        private readonly RepaintEventsView _repaintEvents;

        private ScriptableObject _database;
        private Vector2 _scrollPosition;
        private int _selectedIndex;

        public DatabaseWindow()
        {
            _items = new List<object>();
            _listView = new SelectableListView<object>(o => o.ToString(), GUILayout.Height(300));
            _repaintEvents = new RepaintEventsView(this);
        }

        protected void OnGUI()
        {
            _repaintEvents.OnGUI();
            
            _database = EditorGUILayout.ObjectField("Database", _database, 
                typeof(ScriptableObject), false) as ScriptableObject;

            var showError = _database != null && !DatabaseItemsScanner.CheckValidDatabase(_database);
            if (showError)
            {
                EditorGUILayout.HelpBox("Selected asset is not database.", MessageType.Error);
            }
            
            DatabaseItemsScanner.GetItems(_database, _items);

            _selectedIndex = _listView.OnGUI(_selectedIndex, _items);
            
            if (_selectedIndex < 0)
            {
                return;
            }
            
            var selectedItem = _items[_selectedIndex];
            EditorGUILayout.LabelField(selectedItem.ToString(), new GUIStyle("PreButton"));
        }
    }
}