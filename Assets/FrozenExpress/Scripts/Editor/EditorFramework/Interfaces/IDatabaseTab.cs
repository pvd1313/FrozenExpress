using System;
using UnityEditor;

namespace FrozenExpress.Scripts.Editor.EditorFramework.Interfaces
{
    public interface IDatabaseTab
    {
        Type TargetType { get; }
        
        void OnGadgetGUI(object data);
    }
}