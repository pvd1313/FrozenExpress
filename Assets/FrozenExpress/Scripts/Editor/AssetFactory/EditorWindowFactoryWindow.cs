using System;
using UnityEditor;

namespace FrozenExpress.Scripts.Editor.AssetFactory
{
    public class EditorWindowFactoryWindow : EditorWindow
    {
        private TypeScannerWindow _scannerWindow;

        [MenuItem("Window/Open...", priority = -1)]
        private static void OpenWindow()
        {
            WindowUtil.OpenToolWindow<EditorWindowFactoryWindow>();
        }

        private void OnEnable()
        {
            _scannerWindow = new TypeScannerWindow(typeof(EditorWindow));
        }

        private void OnGUI()
        {
            WindowUtil.InitializeWindowPosition(this);

            if (!_scannerWindow.OnGUI(out var selectedType))
            {
                return;
            }

            EditorWindow.GetWindow(selectedType.Type, false, selectedType.NameShort);
            
            Close();
        }

        private void OnLostFocus()
        {
            Close();
        }
    }
}