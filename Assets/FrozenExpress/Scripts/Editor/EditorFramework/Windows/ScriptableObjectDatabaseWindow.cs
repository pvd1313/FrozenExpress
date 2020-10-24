using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrozenExpress.Scripts.Editor.EditorFramework.Data;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
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
        private readonly SelectableListView<SerializedProperty> _itemListView;
        private readonly TabListView<IDatabaseTab> _tabListView;
        private readonly RepaintEventsView _repaintEvents;
        private readonly List<IDatabaseTab> _tabs;
        private readonly List<Type> _tabTypeBuffer;
        
        private ScriptableObject _database;
        private SerializedObject _databaseSerialized;
        private int _selectedItemIndex;
        private int _selectedTabIndex;
        private Vector2 _itemRawScrollPos;

        public ScriptableObjectDatabaseWindow()
        {
            _tabTypeBuffer = new List<Type>();
            _tabs = new List<IDatabaseTab>();
            _items = new List<SerializedProperty>();
            _itemListView = new SelectableListView<SerializedProperty>(GetPropertyName, GUILayout.Height(200));
            _tabListView = new TabListView<IDatabaseTab>(GetTabName);
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
            if (!databaseInfo.IsDatabase)
            {
                EditorGUILayout.HelpBox("Selected asset is not database", MessageType.Error);
                return;
            }

            if (_databaseSerialized == null || _databaseSerialized.targetObject != _database)
            {
                _databaseSerialized = new SerializedObject(_database);
                DatabaseTabScanner.GetTabs(databaseInfo.ListItemType, _tabTypeBuffer);

                var tabsQuery = from tabType in _tabTypeBuffer
                    select (IDatabaseTab) Activator.CreateInstance(tabType);
                
                _tabs.Clear();
                _tabs.AddRange(tabsQuery);
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

            var selectedItem = listProperty.GetArrayElementAtIndex(_selectedItemIndex);
            
            var wasExpanded = selectedItem.isExpanded;
            selectedItem.isExpanded = true;
            
            UnityEditor.EditorGUI.BeginChangeCheck();
            _itemRawScrollPos = EditorGUILayout.BeginScrollView(_itemRawScrollPos, GUILayout.Height(300));
            EditorGUILayout.PropertyField(selectedItem);
            EditorGUILayout.EndScrollView();
            var anyChanges = UnityEditor.EditorGUI.EndChangeCheck();
            
            if (anyChanges)
            {
                _databaseSerialized.ApplyModifiedProperties();
            }

            selectedItem.isExpanded = wasExpanded;

            EditorGUILayout.LabelField("", separatorStyle);

            EditorGUILayout.BeginHorizontal(GUILayout.Height(30));
            _selectedTabIndex = _tabListView.OnGUI(_selectedTabIndex, _tabs);
            EditorGUILayout.EndHorizontal();

            if (_selectedTabIndex <= 0)
            {
                return;
            }

            var list = (IList) databaseInfo.ListField.GetValue(_database);
            _tabs[_selectedTabIndex].OnGadgetGUI(list[_selectedItemIndex]);
        }

        private static string GetPropertyName(SerializedProperty property)
        {
            return property.displayName;
        }

        private static string GetTabName(IDatabaseTab tab)
        {
            return tab.GetType().Name;
        }
    }
}