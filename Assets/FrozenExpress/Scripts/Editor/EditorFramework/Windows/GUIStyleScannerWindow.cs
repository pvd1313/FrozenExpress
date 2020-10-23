using System.Collections.Generic;
using System.Linq;
using FrozenExpress.Scripts.Editor.EditorFramework.Tools;
using FrozenExpress.Scripts.Editor.EditorFramework.Views;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Windows
{
    public class GUIStyleScannerWindow : EditorWindow
    {
        private static readonly List<GUIStyle> Buffer = new List<GUIStyle>();
        
        private List<GUIStyle> _knownStyles;
        private int _selectedIndex;
        private SelectableListView<GUIStyle> _listView;

        public GUIStyleScannerWindow()
        {
            _listView = new SelectableListView<GUIStyle>(s => s.name, GUILayout.Height(300));
        }
        
        private void OnEnable()
        {
            StaticFieldValueScanner.Scan(typeof(EditorWindow).Assembly, Buffer);
        }

        private void OnGUI()
        {
            if (_knownStyles == null)
            {
                _knownStyles = Buffer.Where(s => !string.IsNullOrWhiteSpace(s.name)).ToList();
            }

            _selectedIndex = _listView.OnGUI(_selectedIndex, _knownStyles);

            EditorGUILayout.Space(10);

            if (_selectedIndex < 0)
            {
                return;
            }

            var selectedStyle = _knownStyles[_selectedIndex];
            GUILayout.TextField(selectedStyle.name, selectedStyle);
        }
    }
}