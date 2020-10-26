using System;
using System.Collections.Generic;
using System.Linq;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using FrozenExpress.Scripts.Game.LevelEditor.Editor;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    partial class ScriptableObjectDatabaseWindow
    {
        public class TabsView
        {
            private readonly Dictionary<Type, List<IDatabaseWindowTab>> _tabs;
            private readonly HorizontalSeparatorView _separatorView;

            private bool _isInitialized;
            private bool _initializationException;
            private DiContainer _tabsContainer;

            public TabsView()
            {
                _separatorView = new HorizontalSeparatorView();
                _tabs = new Dictionary<Type, List<IDatabaseWindowTab>>();
            }

            public void OnGUI(object data)
            {
                _separatorView.OnGUI();
                
                if (!Initialize())
                {
                    return;
                }

                if (!_tabs.TryGetValue(data.GetType(), out var tabList))
                {
                    return;
                }

                foreach (var tab in tabList)
                {
                    tab.OnGUI(data);
                    _separatorView.OnGUI();
                }
            }

            private bool Initialize()
            {
                if (!Application.isPlaying)
                {
                    EditorGUILayout.HelpBox("Start game to show additional tabs", MessageType.Info);
                    
                    _isInitialized = false;
                    _initializationException = false;
                    
                    return false;
                }

                if (_initializationException)
                {
                    return false;
                }
                
                if (_isInitialized)
                {
                    return true;
                }

                try
                {
                    _tabs.Clear();

                    var sceneContext = GameObject.FindObjectOfType<SceneContext>();
                    var gameContainer = sceneContext.Container;
                    var tabsContainer = new DiContainer(gameContainer);
                    
                    tabsContainer.Install<LevelEditorDatabaseTabsInstaller>();
                    
                    var tabGroups = tabsContainer.Resolve<List<IDatabaseWindowTab>>()
                        .GroupBy(t => t.TargetType);
                    
                    foreach (var tabGroup in tabGroups)
                    {
                        _tabs[tabGroup.Key] = tabGroup.ToList();
                    }

                    _isInitialized = true;
                }
                catch (Exception e)
                {
                    _initializationException = true;
                    
                    Debug.LogException(e);
                    
                    return false;
                }

                return true;
            }
        }
    }
}