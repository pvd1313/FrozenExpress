using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Scripts.Editor.AssetFactory
{
    public static class WindowUtil
    {
        public const int WindowWidth = 200;
        private const int WindowHeight = 300;
        
        private static HashSet<EditorWindow> _nonInitializedWindows;

        static WindowUtil()
        {
            _nonInitializedWindows = new HashSet<EditorWindow>();
        }
        
        public static void OpenToolWindow<T>(string title) where T : EditorWindow
        {
            var window = ScriptableObject.CreateInstance<T>();
            _nonInitializedWindows.Add(window);
            window.titleContent = new GUIContent(title);
            window.ShowPopup();
        }

        public static void InitializeWindowPosition(EditorWindow window)
        {
            const int widthHalf = WindowWidth / 2;
            const int heightHalf = WindowHeight / 2;
            
            var currentEvent = Event.current;
            if (currentEvent == null || currentEvent.type == EventType.Ignore)
            {
                return;
            }

            if (_nonInitializedWindows.Remove(window))
            {
                var mPos = currentEvent.mousePosition;
                window.position = new Rect(mPos.x - widthHalf, mPos.y - heightHalf, WindowWidth, WindowHeight);
            }
        }
    }
}