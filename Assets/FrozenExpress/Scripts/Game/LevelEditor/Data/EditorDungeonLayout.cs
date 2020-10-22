using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;

namespace FrozenExpress.Scripts.Game.LevelEditor.Data
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
