using System;
using FrozenExpress.Editor.EditorFramework;
using FrozenExpress.Game.DungeonGenerator;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.DungeonGenerator.Editor
{
    [DatabaseWindowTab(typeof(RoomPattern))]
    public class RoomPatternExampleTab : IDatabaseWindowTab
    {
        public Type TargetType => typeof(RoomPattern);
        public void OnGUI(object data)
        {
            EditorGUILayout.LabelField("Hey bro! What's up?");
            // Handles.PositionHandle(Vector3.zero, Quaternion.identity);
        }
    }
}