using System.Collections.Generic;
using FrozenExpress.Game.Dungeon;
using UnityEngine;

namespace FrozenExpress.Game.DungeonCollision
{
    public class DungeonCollisionController : IDungeonCollisionController
    {
        private readonly IDungeonPatternTransformController _patternTransform;
        private readonly IDungeonTransform _viewTransform;
        private readonly IDungeonEntityDatabase _entityDatabase;

        private readonly Vector3 _scaleOne;
        private readonly Quaternion _rotationIdentity;
        private readonly Transform _root;
        private readonly List<GameObject> _colliders;
        
        public DungeonCollisionController(IDungeonPatternTransformController patternTransform,
                                          IDungeonTransform viewTransform,
                                          IDungeonEntityDatabase entityDatabase)
        {
            _patternTransform = patternTransform;
            _viewTransform = viewTransform;
            _entityDatabase = entityDatabase;

            var rootGameObject = new GameObject(nameof(DungeonCollisionController));
            _root = rootGameObject.transform;
            _colliders = new List<GameObject>();
            _scaleOne = new Vector3(1, 1, 1);
            _rotationIdentity = Quaternion.identity;
        }

        public void InitializeColliders(IEnumerable<EntityPatternTransform> entities)
        {
            DestroyColliders();

            foreach (var entityPattern in entities)
            {
                var entity = _entityDatabase.GetEntity(entityPattern.EntityId);
                if (entity == null || entity.Colliders.Count == 0)
                {
                    continue;
                }

                var colliders = entity.Colliders;
                if (colliders.Count == 0)
                {
                    continue;
                }
                
                var cellPosition = _viewTransform.TransformPosition(entityPattern.Position);
                var cellScale = _scaleOne;
                var cellRotation = _rotationIdentity;
                var transform = entityPattern.Transform;
                for (var i = 0; i < transform.Count; i++)
                {
                    var operation = transform[i];
                    cellScale = _patternTransform.TransformScale(cellScale, operation);
                    cellRotation = _patternTransform.TransformRotation(cellRotation, operation);
                }

                foreach (var collider in colliders)
                {
                    var colliderOffset = cellRotation * Vector3.Scale(cellScale, collider.Position);
                    var colliderRotation = cellRotation * Quaternion.Euler(collider.Rotation);
                    
                    SpawnBoxCollider(cellPosition + colliderOffset, colliderRotation, collider.Size);
                }
            }
        }
        
        private void DestroyColliders()
        {
            foreach (var collider in _colliders)
            {
                GameObject.Destroy(collider);
            }
            
            _colliders.Clear();
        }
        
        private void SpawnBoxCollider(Vector3 position, Quaternion rotation, Vector3 size)
        {
            var obj = new GameObject("Collider_" + _colliders.Count);
            
            var transform = obj.transform;
            transform.position = position;
            transform.rotation = rotation;
            transform.SetParent(_root);
            
            var collider = obj.AddComponent<BoxCollider>();
            collider.size = size;
            
            _colliders.Add(obj);
        }
    }
}