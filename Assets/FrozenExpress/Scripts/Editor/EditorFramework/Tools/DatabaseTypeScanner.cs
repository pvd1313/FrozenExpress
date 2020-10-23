using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using FrozenExpress.Scripts.Editor.EditorFramework.Data;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Tools
{
    public static class DatabaseTypeScanner
    {
        private static readonly Dictionary<Type, DatabaseTypeInfo> Cash;
        private static readonly Type ListType = typeof(IList);

        static DatabaseTypeScanner()
        {
            Cash = new Dictionary<Type, DatabaseTypeInfo>();
        }
        
        public static DatabaseTypeInfo GetInfo(Type type)
        {
            if (Cash.TryGetValue(type, out var info))
            {
                return info;
            }

            info = Scan(type);
            Cash[type] = info;

            return info;
        }

        private static DatabaseTypeInfo Scan(Type type)
        {
            DatabaseTypeInfo result;
            result.Type = type;
            result.ItemListField = null;

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;

                if (!ListType.IsAssignableFrom(fieldType))
                {
                    continue;
                }

                result.ItemListField = field;
                
                break;
            }

            return result;
        }
    }
}