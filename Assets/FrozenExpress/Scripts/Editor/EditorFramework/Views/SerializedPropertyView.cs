using UnityEditor;

namespace FrozenExpress.Editor.EditorFramework
{
    public class SerializedPropertyView
    {
        private bool _isExpanded = true;
        
        public void OnGUI(SerializedProperty property)
        {
            property.serializedObject.Update();
            
            var wasExpanded = property.isExpanded;
            
            property.isExpanded = _isExpanded;
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(property);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            _isExpanded = property.isExpanded;

            property.isExpanded = wasExpanded;
        }
    }
}