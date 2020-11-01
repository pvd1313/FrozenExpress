using System;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    public interface IDatabaseWindowTab
    {
        Type TargetType { get; } 
        
        void OnGUI(object data);

        void OnSceneGUI(Camera camera, object data);
    }
}