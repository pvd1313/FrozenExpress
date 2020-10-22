using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.ScriptableObjectCreation
{
    public partial class ScriptableObjectFactory : EditorWindow
    {
        private const int MaxAssetNameGenerationIterations = 1024;

        private string _outputPath;
        private TypeScannerWindow _scannerWindow;

        [MenuItem("Assets/Create Scriptable Object...")]
        private static void InvokeWindow()
        {
            GetWindow(typeof(ScriptableObjectFactory), true);
        }

        private void OnEnable()
        {
            _outputPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            _scannerWindow = new TypeScannerWindow(typeof(ScriptableObject));
        }

        private void OnGUI()
        {
            EditorGUILayout.TextField("Output folder:", _outputPath);

            if (!_scannerWindow.OnGUI(out var selectedType))
            {
                return;
            }

            var asset = CreateAsset(_outputPath, selectedType.Type);
            if (asset == null)
            {
                return;
            }

            Selection.activeObject = asset;
            Close();
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
                var asset = ScriptableObject.CreateInstance(type);
                AssetDatabase.CreateAsset(asset, path);
                return asset;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }

        private static bool CheckAssetExists(string path)
        {
            return !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path));
        }
    }
}
