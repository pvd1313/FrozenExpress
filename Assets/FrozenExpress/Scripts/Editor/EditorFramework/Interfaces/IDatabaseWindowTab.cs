using System;
using UnityEditor;

namespace FrozenExpress.Editor.EditorFramework
{
    public interface IDatabaseWindowTab
    {
        Type TargetType { get; } 
        
        void OnGUI(object data);

        void OnSceneGUI(SceneView sceneView, object data);
    }
}