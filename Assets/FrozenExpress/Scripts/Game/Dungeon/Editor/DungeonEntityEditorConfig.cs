using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Editor
{
    public class DungeonEntityEditorConfig : ScriptableObject
    {
        public Material EntityMaterial => _entityMaterial;
        public Vector3 RoomSize => _roomSize;
        public Color RoomBoundsColor => _roomBoundsColor;
        public Material ColliderMaterial => _colliderMaterial;
        public Mesh CubeMesh => _cubeMesh;

        [SerializeField] private Color _roomBoundsColor;
        [SerializeField] private Material _entityMaterial;
        [SerializeField] private Material _colliderMaterial;
        [SerializeField] private Mesh _cubeMesh;
        [SerializeField] private Vector3 _roomSize;
    }
}