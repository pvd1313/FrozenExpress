using FrozenExpress.Game.DungeonLayout;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.LevelEditor
{
    public class EditorDungeonLayout : DungeonLayout
    {
        public EditorDungeonLayout()
        {
            _cells = new List<UnityEngine.Vector3Int>();
        }

        public void Initialize(ILevelEditorModel dungeonmodel)
        {
            _cells.Clear();
            _cells.AddRange(dungeonmodel.Cells);
        }
    }
}
