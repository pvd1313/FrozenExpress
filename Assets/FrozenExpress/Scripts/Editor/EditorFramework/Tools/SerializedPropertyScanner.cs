// using System;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// namespace FrozenExpress.Scripts.Editor.EditorFramework.Tools
// {
//     public class SerializedPropertyScanner
//     {
//         private readonly Dictionary<string, string> _propertyToField;
//         private readonly Dictionary<string, SerializedProperty> _fields;
//             
//         public SerializedPropertyScanner(params string[] propertyNames)
//         {
//             _propertyToField = new Dictionary<string, string>();
//             _fields = new Dictionary<string, SerializedProperty>();
//             foreach (var propertyName in propertyNames)
//             {
//                 _propertyToField[propertyName] = PropertyToField(propertyName);
//             }
//         }
//
//         public bool Scan(SerializedProperty property)
//         {
//             _fields.Clear();
//             foreach (var fieldName in _propertyToField.Values)
//             {
//                 var field = property.FindPropertyRelative(fieldName);
//                 if (field == null)
//                 {
//                     Debug.LogError($"{GetType().Name} :: Field '{fieldName}' is not found in {property.type}");
//                     return false;
//                 }
//
//                 _fields[fieldName] = field;
//             }
//
//             return true;
//         }
//             
//         public SerializedProperty this[string propertyName]
//         {
//             get
//             {
//                 var fieldName = _propertyToField[propertyName];
//                 return _fields[fieldName];
//             }
//         }
//
//         private string PropertyToField(string propertyName)
//         {
//             return "_" + Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
//         }
//     }
// }