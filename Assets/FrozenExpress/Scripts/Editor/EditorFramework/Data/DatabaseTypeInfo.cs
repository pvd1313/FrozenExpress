using System;
using System.Reflection;
using UnityEditor;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Data
{
    public struct DatabaseTypeInfo
    {
        public bool IsDatabase;
        public Type ListItemType;
        public FieldInfo ListField;
        public string ListName;
    }
}