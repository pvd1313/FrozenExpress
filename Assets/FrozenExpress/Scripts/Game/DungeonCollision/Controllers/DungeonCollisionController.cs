using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonCollision
{
    public class DungeonCollisionController : IDungeonCollisionController
    {
        private readonly GameObject _root;
        private readonly List<GameObject> _colliders;
        
        public DungeonCollisionController()
        {
            _root = new GameObject(nameof(DungeonCollisionController));
            _colliders = new List<GameObject>();
        }
        
        public void SpawnBoxCollider(Matrix4x4 matrix)
        {
            var obj = new GameObject("Collider_" + _colliders.Count);
            
            var transform = obj.transform;
            transform.position = matrix.MultiplyPoint(Vector3.zero);
            transform.rotation = matrix.rotation;
            transform.localScale = matrix.MultiplyVector(Vector3.one);
            
            var collider = obj.AddComponent<BoxCollider>();
            collider.center = Vector3.zero;
            collider.size = Vector3.one;
            
            _colliders.Add(obj);
        }

        public void ClearColliders()
        {
            foreach (var collider in _colliders)
            {
                GameObject.Destroy(collider);
            }
            
            _colliders.Clear();
        }
    }
}