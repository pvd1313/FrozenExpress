using FrozenExpress.Scripts.Editor.EditorFramework.Data;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Views
{
    public class HorizontalSeparatorView
    {
        private GUIStyle _style;

        public void OnGUI()
        {
            if (_style == null)
            {
                _style = new GUIStyle(GUIStylesConfig.HorizontalSeparator);
            }
            
            EditorGUILayout.LabelField("", _style);
        }
    }
}