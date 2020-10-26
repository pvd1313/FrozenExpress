using System;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Interfaces
{
    public interface IDatabaseWindowTab
    {
        Type TargetType { get; } 
        
        void OnGUI(object data);
    }
}