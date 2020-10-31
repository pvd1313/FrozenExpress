using System;

namespace FrozenExpress.Editor.EditorFramework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseWindowTabAttribute : Attribute
    {
        public Type Target { get; }

        public DatabaseWindowTabAttribute(Type target)
        {
            Target = target;
        }
    }
}