using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Debugging.Interfaces;
using FrozenExpress.Scripts.Game.Debugging.Views;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    public class GizmosDrawingController : IDebugDrawingController, ITerminatable
    {
        private struct CubeDrawTask
        {
            public Vector3 Position;
            public Vector3 Size;
            public Color32 Color;
            public float ExpireTime;
        }
        
        private readonly GizmosDrawingView _gizmosDrawingView;
        private readonly List<CubeDrawTask> _cubeDrawTasks;

        private float _unscaledTime;

        public GizmosDrawingController(GizmosDrawingView gizmosDrawingView)
        {
            _gizmosDrawingView = gizmosDrawingView;
            _cubeDrawTasks = new List<CubeDrawTask>();

            _gizmosDrawingView.OnGizmosDraw += DrawGizmos;
        }
        
        public void DrawCube(Vector3 position, Vector3 size, Color32 color, float duration)
        {
            CubeDrawTask task;
            task.Position = position;
            task.Size = size;
            task.Color = color;
            task.ExpireTime = _unscaledTime + duration;
            
            _cubeDrawTasks.Add(task);    
        }

        private void DrawGizmos()
        {
            _unscaledTime = Time.unscaledTime;
            
            for (var i = _cubeDrawTasks.Count - 1; i >= 0; i--)
            {
                var task = _cubeDrawTasks[i];
                Gizmos.color = task.Color;
                Gizmos.DrawCube(task.Position, task.Size);

                if (_unscaledTime >= task.ExpireTime)
                {
                    _cubeDrawTasks.RemoveAt(i);
                }
            }
        }

        public void Terminate()
        {
            _gizmosDrawingView.OnGizmosDraw -= DrawGizmos;
        }
    }
}