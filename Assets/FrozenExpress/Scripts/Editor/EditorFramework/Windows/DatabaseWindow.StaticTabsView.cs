using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EditorGUI = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUI;

namespace FrozenExpress.Editor.EditorFramework
{
    partial class DatabaseWindow
    {
        private class StaticTabsView
        {
            private readonly Dictionary<Type, List<IDatabaseWindowTab>> _tabs;
            private readonly Dictionary<IDatabaseWindowTab, bool> _foldoutStates;
            private readonly HorizontalSeparatorView _separatorView;
            private readonly Dictionary<IDatabaseWindowTab, string> _tabNames;

            public StaticTabsView()
            {
                _tabs = new Dictionary<Type, List<IDatabaseWindowTab>>();
                _separatorView = new HorizontalSeparatorView();
                _foldoutStates = new Dictionary<IDatabaseWindowTab, bool>();
                _tabNames = new Dictionary<IDatabaseWindowTab, string>();
            }

            public void OnGUI(object data)
            {
                var tabs = GetOrCreateTabs(data.GetType());
                foreach (var tab in tabs)
                {
                    _separatorView.OnGUI();
                    
                    _foldoutStates.TryGetValue(tab, out var foldoutState);
                    foldoutState = EditorGUILayout.Foldout(foldoutState, _tabNames[tab]);
                    _foldoutStates[tab] = foldoutState;

                    if (foldoutState)
                    {
                        UnityEditor.EditorGUI.indentLevel++;
                        tab.OnGUI(data);
                        UnityEditor.EditorGUI.indentLevel--;
                    }
                }
            }

            private IEnumerable<IDatabaseWindowTab> GetOrCreateTabs(Type dataType)
            {
                if (_tabs.TryGetValue(dataType, out var tabList))
                {
                    return tabList;
                }
                
                var knownTabs = DatabaseWindowTabScanner.GetTabs(dataType);
                tabList = new List<IDatabaseWindowTab>();
                foreach (var tabType in knownTabs)
                {
                    try
                    {
                        var tabInstance = Activator.CreateInstance(tabType);
                        if (tabInstance is IDatabaseWindowTab tabInstanceCasted)
                        {
                            tabList.Add(tabInstanceCasted);
                            _tabNames[tabInstanceCasted] = tabType.Name;
                            continue;
                        }
                        
                        Debug.LogError($"Tab '{tabType}' does not implement '{nameof(IDatabaseWindowTab)}'");
                    }
                    catch (Exception e)
                    {
                        LogError($"Failed to create tab '{tabType}'");
                        Debug.LogException(e);
                    }
                }

                _tabs[dataType] = tabList;
                
                return tabList;
            }

            private void LogError(string message)
            {
                Debug.LogError($"${nameof(StaticTabsView)} :: {message}");
            }
        }
    }
}