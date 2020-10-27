using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Debugging.Data;
using FrozenExpress.Scripts.Game.Debugging.Enums;
using FrozenExpress.Scripts.Game.Debugging.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Controllers
{
    public class UnityDebugDrawer : ADebugDrawer
    {
        private static readonly DebugShape DefaultShape;

        static UnityDebugDrawer()
        {
            DebugShape shape;
            shape.Shader = EDebugShader.Diffuse;
            shape.Shape = EDebugShape.Sphere3D;
            shape.Position = Vector3.zero;
            shape.Rotation = Quaternion.identity;
            shape.Scale = Vector3.one;
            shape.Color = EDebugColor.White;
            shape.Duration = 0;

            DefaultShape = shape;
        }
        
        private readonly object _owner;
        private readonly IDebugDrawingController _drawingController;

        private DebugShape _defaultShape;
        
        public UnityDebugDrawer(object owner, IDebugDrawingController drawingController)
        {
            _owner = owner;
            _drawingController = drawingController;
            _defaultShape = DefaultShape;
        }

        public override void Draw(Vector3 position)
        {
            var shape = _defaultShape;
            shape.Position = position;
            
            _drawingController.DrawShape(shape);
        }

        public override void Draw(Vector3 position, Quaternion rotation)
        {
            var shape = _defaultShape;
            shape.Position = position;
            shape.Rotation = rotation;
            
            _drawingController.DrawShape(shape);
        }

        public override void SetColor(EDebugColor color)
        {
            _defaultShape.Color = color;
        }

        public override void SetShape(EDebugShape shape)
        {
            _defaultShape.Shape = shape;
        }

        public override void SetScale(Vector3 scale)
        {
            _defaultShape.Scale = scale;
        }

        public override void SetDuration(float duration)
        {
            _defaultShape.Duration = duration;
        }
    }
}