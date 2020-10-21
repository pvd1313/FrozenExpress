using FrozenExpress.Game.DungeonLayout;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public class EditorDungeonLayoutData : DungeonLayout.DungeonLayout
    {
        public EditorDungeonLayoutData()
        {
            _cells = new List<UnityEngine.Vector3Int>();
        }

        public void Initialize(IDungeonLayoutEditorModel dungeonmodel)
        {
            _cells.Clear();
            _cells.AddRange(dungeonmodel.Cells);
        }
    }
}
