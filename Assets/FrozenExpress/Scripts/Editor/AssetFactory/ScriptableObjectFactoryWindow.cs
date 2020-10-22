using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.AssetFactory
{
    public partial class ScriptableObjectFactoryWindow : EditorWindow
    {
        private string _outputPath;
        private TypeScannerWindow _scannerWindow;
        private bool _initializedPosition;

        [MenuItem("Assets/Create ScriptableObject...")]
        private static void InvokeWindow()
        {
            WindowUtil.OpenToolWindow<ScriptableObjectFactoryWindow>("Create scriptable object");
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
