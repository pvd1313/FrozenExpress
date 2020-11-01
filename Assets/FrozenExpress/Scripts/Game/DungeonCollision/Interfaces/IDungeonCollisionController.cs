using UnityEngine;

namespace FrozenExpress.Game.DungeonCollision
{
    public interface IDungeonCollisionController
    {
        void SpawnBoxCollider(Matrix4x4 matrix);
        
        void ClearColliders();
    }
}