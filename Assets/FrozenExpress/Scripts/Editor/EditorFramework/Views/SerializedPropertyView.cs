using UnityEditor;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Views
{
    public class SerializedPropertyView
    {
        public void OnGUI(SerializedProperty property)
        {
            property.serializedObject.Update();
            
            var wasExpanded = property.isExpanded;
            property.isExpanded = true;
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(property);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            property.isExpanded = wasExpanded;
        }
    }
}