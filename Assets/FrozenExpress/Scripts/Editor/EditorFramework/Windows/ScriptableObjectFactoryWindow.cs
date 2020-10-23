using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public class ScriptableObjectFactoryWindow : EditorWindow
    {
        private string _outputPath;
        private TypeScannerWindow _scannerWindow;

        [MenuItem("Assets/Create asset...")]
        private static void InvokeWindow()
        {
            WindowUtil.OpenToolWindow<ScriptableObjectFactoryWindow>();
        }

        private void OnEnable()
        {
            _outputPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            _scannerWindow = new TypeScannerWindow(typeof(ScriptableObject));
        }

        private void OnGUI()
        {
            WindowUtil.InitializeWindowPosition(this);

            if (!_scannerWindow.OnGUI(out var selectedType))
            {
                return;
            }

            var asset = ScriptableObjectFactory.CreateAsset(_outputPath, selectedType.Type);
            
            Selection.activeObject = asset;
            
            Close();
        }

        private void OnLostFocus()
        {
            Close();
        }
    }
}
