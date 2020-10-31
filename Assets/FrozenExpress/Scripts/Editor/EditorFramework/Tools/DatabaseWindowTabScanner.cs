using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    public static class DatabaseWindowTabScanner
    {
        private static readonly IDictionary<Type, List<Type>> Tabs;

        static DatabaseWindowTabScanner()
        {
            try
            {
                Tabs = new Dictionary<Type, List<Type>>();
                var editorTabType = typeof(IDatabaseWindowTab);
                var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                    .Where(t => editorTabType.IsAssignableFrom(t));
                
                foreach (var type in allTypes)
                {
                    var attribute = Attribute.GetCustomAttributes(type)
                        .Select(a => a as DatabaseWindowTabAttribute)
                        .FirstOrDefault();

                    if (attribute == null)
                    {
                        continue;
                    }

                    Tabs.TryGetValue(attribute.Target, out var list);
                    if (list == null)
                    {
                        list = new List<Type>();
                        Tabs[attribute.Target] = list;
                    }
                    
                    list.Add(type);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static IEnumerable<Type> GetTabs(Type dataType)
        {
            if (Tabs.TryGetValue(dataType, out var result))
            {
                return result;
            }

            return Enumerable.Empty<Type>();
        }
    }
}