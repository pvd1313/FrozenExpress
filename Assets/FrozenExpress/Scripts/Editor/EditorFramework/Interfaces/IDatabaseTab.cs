using System;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Interfaces
{
    public interface IDatabaseTab
    {
        Type TargetType { get; }
        
        void OnGUI(object data);
        void OnRender(Camera camera);
    }
}