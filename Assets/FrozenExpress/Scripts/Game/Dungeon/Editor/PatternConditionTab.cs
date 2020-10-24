using System;
using FrozenExpress.Scripts.Editor.EditorFramework.Interfaces;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Editor
{
    public class PatternConditionTab : IDatabaseTab
    {
        public Type TargetType => typeof(Pattern);

        private Material _material;
        private Mesh _mesh;

        public void OnGUI(object data)
        {
            if (data is Pattern pattern)
            {
                _material = EditorGUILayout.ObjectField("Material", _material, typeof(Material), false) as Material;
                _mesh = EditorGUILayout.ObjectField("Mesh", _mesh, typeof(Mesh), false) as Mesh;
            }
        }

        public void OnRender(Camera camera)
        {
            if (!_material || !_mesh)
            {
                return;
            }

            var matrix = Matrix4x4.TRS(Vector3.forward * 10, Quaternion.identity, Vector3.one);
            Graphics.DrawMesh(_mesh, camera.transform.localToWorldMatrix * matrix, _material, 4, camera);
        }
    }
}