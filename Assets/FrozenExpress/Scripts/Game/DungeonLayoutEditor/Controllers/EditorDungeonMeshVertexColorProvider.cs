using FrozenExpress.Game.DungeonLayout;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public class EditorDungeonMeshVertexColorProvider : IDungeonMeshVertexColorProvider
    {
        private readonly List<DungeonMeshVertexColor> _vertexColors;
        private readonly Color32 _colorWhite;

        public EditorDungeonMeshVertexColorProvider(List<DungeonMeshVertexColor> vertexColors)
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
