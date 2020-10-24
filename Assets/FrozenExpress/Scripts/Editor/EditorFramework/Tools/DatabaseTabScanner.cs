using System;
using System.Collections.Generic;
using System.Linq;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Tools
{
    public static class DatabaseTabScanner
    {
        private static readonly Dictionary<Type, List<Type>> Cash;

        static DatabaseTabScanner()
        {
            var tabEditorType = typeof(IDatabaseTab);
            
            var tabs = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => tabEditorType.IsAssignableFrom(t))
                .Where(t => !t.IsAbstract && t.IsClass);
            
            Cash = new Dictionary<Type, List<Type>>();

            foreach (var tabType in tabs)
            {
                try
                {
                    var tabInstance = (IDatabaseTab) Activator.CreateInstance(tabType);
                    var targetType = tabInstance.TargetType;
                    
                    if (!Cash.TryGetValue(targetType, out var tabList))
                    {
                        tabList = new List<Type>();
                        Cash[targetType] = tabList;
                    }
                    
                    tabList.Add(tabType);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
        
        public static void GetTabs(Type targetObject, List<Type> result)
        {
            if (targetObject == null)
            {
                throw new ArgumentNullException(nameof(targetObject));
            }

            result.Clear();
            
            if (Cash.TryGetValue(targetObject, out var cashed))
            {
                result.AddRange(cashed);
            }
        }
    }
}