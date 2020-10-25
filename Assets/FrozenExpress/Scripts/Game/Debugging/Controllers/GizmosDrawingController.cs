using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Debugging.Interfaces;
using FrozenExpress.Scripts.Game.Debugging.Views;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    public class GizmosDrawingController : IDebugDrawingController, ITerminatable
    {
        private readonly GizmosDrawingView _gizmosDrawingView;
        private readonly List<(Vector3, Vector3, Color32)> _cubeDrawTasks;

        public GizmosDrawingController(GizmosDrawingView gizmosDrawingView)
        {
            _gizmosDrawingView = gizmosDrawingView;
            _cubeDrawTasks = new List<(Vector3, Vector3, Color32)>();

            _gizmosDrawingView.OnGizmosDraw += DrawGizmos;
        }
        
        public void DrawCube(Vector3 position, Vector3 size, Color32 color)
        {
            var task = (position, size, color);
            _cubeDrawTasks.Add(task);    
        }

        private void DrawGizmos()
        {
            foreach (var task in _cubeDrawTasks)
            {
                Gizmos.color = task.Item3;
                Gizmos.DrawCube(task.Item1, task.Item2);
            }
            
            _cubeDrawTasks.Clear();
        }

        public void Terminate()
        {
            _gizmosDrawingView.OnGizmosDraw -= DrawGizmos;
        }
    }
}