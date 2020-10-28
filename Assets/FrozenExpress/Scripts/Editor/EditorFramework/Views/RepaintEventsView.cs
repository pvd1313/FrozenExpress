using UnityEditor;
using UnityEngine;

namespace FrozenExpress.Editor.EditorFramework
{
    public class RepaintEventsView
    {
        private readonly EditorWindow _owner;

        public RepaintEventsView(EditorWindow owner)
        {
            _owner = owner;
        }
        
        public void OnGUI()
        {
            var curEvent = Event.current;
            
            if (curEvent != null && curEvent.type == EventType.KeyDown)
            {
                _owner.Repaint();
            }
        }
    }
}