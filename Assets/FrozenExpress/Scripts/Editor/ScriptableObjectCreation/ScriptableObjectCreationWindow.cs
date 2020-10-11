using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.ScriptableObjectCreation
{
    public partial class ScriptableObjectCreationWindow : EditorWindow
    {
        private const int MaxAssetNameGenerationIterations = 1000;

        private string _outputPath;
        private string _filter;
        private bool _filterForceFocusedOnStart;
        private List<TypeData> _filteredAssets;
        private Vector2 _scrollPos;

        [MenuItem("Assets/Create Scriptable Object...")]
        private static void InvokeWindow()
        {
            GetWindow(typeof(ScriptableObjectCreationWindow), true);
        }

        private void OnEnable()
        {
            _outputPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            _filteredAssets = new List<TypeData>();
            _filter = string.Empty;
        }

        private void OnGUI()
        {
            EditorGUILayout.TextField("Output folder:", _outputPath);

            GUI.SetNextControlName(nameof(_filter));
            _filter = EditorGUILayout.TextField("Search: ", _filter);
            TypeScanner.SearchAssets(_filter, _filteredAssets);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            foreach (var item in _filteredAssets)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    var asset = CreateAsset(_outputPath, item.Type);
                    if (asset != null)
                    {
                        Selection.activeObject = asset;
                        Close();
                    }
                }

                EditorGUILayout.LabelField(item.NameShort);
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            if (!_filterForceFocusedOnStart)
            {
                _filterForceFocusedOnStart = true;
                GUI.FocusControl(nameof(_filter));
            }
        }

        private UnityEngine.Object CreateAsset(string directory, Type type)
        {
            var path = Path.Combine(directory, type.Name + ".asset");

            if (CheckAssetExists(path))
            {
                path = Path.Combine(directory, type.Name + " - new.asset");
            }

            for (var i = 0; CheckAssetExists(path); i++)
            {
                if (i >= MaxAssetNameGenerationIterations)
                {
                    Debug.LogError($"Failed to create asset '{type}' at '{directory}'. All names are occupied");
                    return null;
                }
                
                path = Path.Combine(directory, type.Name + " - new (" + i + ").asset");
            }

            try
            {
                var asset = Activator.CreateInstance(type) as UnityEngine.Object;
                AssetDatabase.CreateAsset(asset, path);
                return asset;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }

        private bool CheckAssetExists(string path)
        {
            return !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path));
        }
    }
}
