using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Editor.ScriptableObjectCreation
{
    partial class ScriptableObjectCreationWindow
    {
        public static class TypeScanner
        {
            private static List<TypeData> _database = new List<TypeData>();

            static TypeScanner()
            {
                try
                {
                    var scriptableObject = typeof(ScriptableObject);
                    _database = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                        .Where(t => scriptableObject.IsAssignableFrom(t))
                        .Select(t => new TypeData()
                        {
                            Type = t,
                            NameShort = t.Name,
                            NameShortLowered = t.Name.ToLowerInvariant(),
                            Namespace = t.Namespace,
                        }).ToList();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            public static void SearchAssets(string namePart, List<TypeData> result)
            {
                namePart = namePart.ToLowerInvariant();
                result.Clear();
                result.AddRange(_database.Where(t => t.NameShortLowered.Contains(namePart)));
            }
        }
    }
}
