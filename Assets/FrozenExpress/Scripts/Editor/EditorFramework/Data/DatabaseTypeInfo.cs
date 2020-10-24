using System;
using System.Reflection;
using UnityEditor;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Data
{
    public struct DatabaseTypeInfo
    {
        public Type Type;
        public FieldInfo ItemListField;
        public string SerializedPropertyName;
    }
}