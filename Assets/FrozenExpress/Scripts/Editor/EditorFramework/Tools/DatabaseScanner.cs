using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FrozenExpress.Editor.EditorFramework
{
    public static class DatabaseScanner
    {
        private static readonly Dictionary<Type, DatabaseTypeInfo> Cash;

        static DatabaseScanner()
        {
            Cash = new Dictionary<Type, DatabaseTypeInfo>();
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

            result = Scan(database);

            Cash[databaseType] = result;

            return result;
        }

        private static DatabaseTypeInfo Scan(Object database)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            var databaseType = database.GetType();
            
            DatabaseTypeInfo result;
            result.IsDatabase = false;
            result.ListName = null;
            result.ListField = null;

            var databaseSerialized = new SerializedObject(database);

            var serializableFieldsQuery = from field in databaseType.GetFields(flags)
                let prop = databaseSerialized.FindProperty(field.Name)
                where prop != null
                select (field, prop);
            
            var serializableFields = serializableFieldsQuery.ToList();

            if (serializableFields.Count == 0)
            {
                LogWarning($"Type '{databaseType}' has no serialized fields.");
                return result;
            }

            if (serializableFields.Count > 1)
            {
                LogWarning($"Type '{databaseType}' has more than one serialized field.");
                return result;
            }
            
            var property = serializableFields[0].prop;
            if (!property.isArray)
            {
                LogWarning($"Field '{databaseType}.{property.name}' is not list.");
                return result;
            }
            
            var genericArguments = serializableFields[0].field.FieldType.GetGenericArguments();
            if (genericArguments.Length == 0)
            {
                LogWarning($"Field '{databaseType}.{property.name}' is not generic");
                return result;
            }
            
            result.IsDatabase = true;
            result.ListName = property.name;
            result.ListField = serializableFields[0].field;

            return result;
        }

        private static void LogWarning(string message)
        {
            Debug.LogWarning($"{nameof(DatabaseScanner)} :: {message}");
        }
    }
}