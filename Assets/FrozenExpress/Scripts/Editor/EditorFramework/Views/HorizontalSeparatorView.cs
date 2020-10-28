using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    public class HorizontalSeparatorView
    {
        private GUIStyle _style;

        public void OnGUI()
        {
            GUILayout.Space(20);
            
            if (_style == null)
            {
                _style = new GUIStyle(GUIStylesConfig.HorizontalSeparator);
            }
            
            EditorGUILayout.LabelField("", _style);
        }
    }
}