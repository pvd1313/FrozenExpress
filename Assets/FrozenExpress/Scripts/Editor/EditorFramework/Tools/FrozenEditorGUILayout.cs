using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    public static class FrozenEditorGUILayout
    {
        public static T AssetField<T>(string label, T asset) where T : Object
        {
            return EditorGUILayout.ObjectField(label, asset, typeof(T), false) as T;
        }
    }
}