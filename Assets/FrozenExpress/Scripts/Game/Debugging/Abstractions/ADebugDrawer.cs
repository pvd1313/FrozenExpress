using UnityEngine;

namespace FrozenExpress.Game.Debugging
{
    public abstract class ADebugDrawer
    {
        public abstract void Draw(Vector3 position);

        public abstract void Draw(Vector3 position, Quaternion rotation);

        public abstract void SetColor(EDebugColor color);

        public abstract void SetShape(EDebugShape shape);

        public abstract void SetDuration(float duration);

        public abstract void SetScale(Vector3 scale);
    }
}