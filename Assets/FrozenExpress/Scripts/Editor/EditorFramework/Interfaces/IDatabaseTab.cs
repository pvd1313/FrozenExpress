using System;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Interfaces
{
    public interface IDatabaseTab
    {
        Type TargetType { get; }
        
        void OnGUI(object data);
    }
}