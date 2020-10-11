using System;

namespace FrozenExpress.Editor.ScriptableObjectCreation
{
    partial class ScriptableObjectCreationWindow
    {
        public struct TypeData
        {
            public Type Type { get; set; }
            public string NameShort { get; set; }
            public string NameShortLowered { get; set; }
            public string Namespace { get; set; }
        }
    }
}
