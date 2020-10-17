using FrozenExpress.Game.Core.DungeonLayout;
using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayoutEditor
{
    public class EditorDungeonLayoutData : DungeonLayoutData
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
