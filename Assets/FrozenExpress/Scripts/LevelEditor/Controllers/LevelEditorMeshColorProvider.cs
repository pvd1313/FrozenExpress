using FrozenExpress.Game.DungeonLayout;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.LevelEditor
{
    public class LevelEditorMeshColorProvider : IDungeonMeshVertexColorProvider
    {
        private readonly List<MeshVertexColor> _vertexColors;
        private readonly Color32 _colorWhite;

        public LevelEditorMeshColorProvider(List<MeshVertexColor> vertexColors)
        {
            _vertexColors = vertexColors;
            _colorWhite = new Color32(1, 1, 1, 1);
        }

        public Color32 GetColor(Mesh mesh)
        {
            for (var i = 0; i < _vertexColors.Count; i++)
            {
                var meshData = _vertexColors[i];
                if (meshData.Mesh == mesh)
                {
                    return meshData.Color;
                }
            }

            return _colorWhite;
        }
    }
}
