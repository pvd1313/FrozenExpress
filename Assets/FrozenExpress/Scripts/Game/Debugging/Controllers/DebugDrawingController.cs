using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Debugging.Data;
using FrozenExpress.Scripts.Game.Debugging.Enums;
using FrozenExpress.Scripts.Game.Debugging.Interfaces;
using FrozenExpress.Scripts.Game.GameCycle.Data;
using FrozenExpress.Scripts.Game.GameCycle.Enums;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    public partial class DebugDrawingController : IDebugDrawingController, IFrameUpdatable
    {
        private struct DrawingTask
        {
            public Material Material;
            public Matrix4x4 Matrix4X4;
            public float ExpireTime;
            public Mesh Mesh;
        }
        
        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");
        
        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly List<DrawingTask> _tasks;
        private readonly bool _initialized;
        
        private int _colorCount;
        private Mesh[] _meshes;
        private Shader[] _shaders;
        private Color[] _colors;
        private Material[] _materials;
        private bool[] _materialInitializationMap;
        
        private float _time;

        public DebugDrawingController(DebugDrawingConfig config)
        {
            _tasks = new List<DrawingTask>();
            _initialized = CashInitializer.TryInititialize(this, config);
        }
        
        public void Update(FrameUpdateData frameData)
        {
            if (!_initialized)
            {
                _tasks.Clear();
                return;
            }
            
            _time = frameData.UnscaledTime;

            for (var i = _tasks.Count - 1; i >= 0; i--)
            {
                var task = _tasks[i];
                
                Graphics.DrawMesh(task.Mesh, task.Matrix4X4, task.Material, 1);

                if (_time >= task.ExpireTime)
                {
                    _tasks.RemoveAt(i);
                }
            }
        }

        public void DrawShape(DebugShape shape)
        {
            if (!_initialized)
            {
                return;
            }

            DrawingTask task;
            task.Material = GetOrCreateMaterial(shape.Shader, shape.Color);
            task.Matrix4X4 = Matrix4x4.TRS(shape.Position, shape.Rotation, shape.Scale);
            task.ExpireTime = _time + shape.Duration;
            task.Mesh = _meshes[(int) shape.Shape];
            
            _tasks.Add(task);
        }

        private Material GetOrCreateMaterial(EDebugShader shader, EDebugColor color)
        {
            var shaderIndex = (int) shader;
            var colorIndex = (int) color;
            var materialIndex = shaderIndex * _colorCount + colorIndex;
            if (_materialInitializationMap[materialIndex])
            {
                return _materials[materialIndex];
            }
            
            var material = new Material(_shaders[shaderIndex]);
            material.SetColor(ColorPropertyId, _colors[colorIndex]);
            
            _materials[materialIndex] = material;
            _materialInitializationMap[materialIndex] = true;

            return material;
        }
    }
}