using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrozenExpress.Scripts.Editor.EditorFramework.Data;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Tools
{
    public static class DatabaseScanner
    {
        private static readonly Dictionary<Type, DatabaseTypeInfo> Cash;
        private static readonly Type ListType;
        private static readonly List<string> PropertyNameBuffer;

        static DatabaseScanner()
        {
            Cash = new Dictionary<Type, DatabaseTypeInfo>();
            ListType = typeof(IList);
            PropertyNameBuffer = new List<string>();
        }

        public static DatabaseTypeInfo GetInfo(Object database)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            var databaseType = database.GetType();
            if (Cash.TryGetValue(databaseType, out var result))
            {
                return result;
            }

            var databaseSerialized = new SerializedObject(database);

            var fields = databaseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyNameBuffer.Clear();
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                if (!ListType.IsAssignableFrom(fieldType))
                {
                    continue;
                }

                var property = databaseSerialized.FindProperty(field.Name);
                if (property == null)
                {
                    continue;
                }
                
                PropertyNameBuffer.Add(field.Name);
            }
            
            result.Type = databaseType;
            result.SerializedPropertyName = null;
            result.ItemListField = null;
            
            if (PropertyNameBuffer.Count > 1)
            {
                Debug.LogWarning($"{nameof(DatabaseScanner)} :: Type '{databaseType}' has more than one serialized list. Ambiguous match.");
            }
            else if (PropertyNameBuffer.Count == 1)
            {
                result.SerializedPropertyName = PropertyNameBuffer[0];
            }

            Cash[databaseType] = result;

            return result;
        }
    }
}