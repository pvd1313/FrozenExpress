using System;

namespace FrozenExpress.Editor.EditorFramework
{
    public interface IDatabaseWindowTab
    {
        Type TargetType { get; } 
        
        void OnGUI(object data);

        void OnSceneGUI(object data);
    }
}