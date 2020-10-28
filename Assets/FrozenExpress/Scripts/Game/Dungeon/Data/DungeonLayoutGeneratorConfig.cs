using UnityEngine;

namespace FrozenExpress.Scripts.Game.Dungeon.Data
{
    [System.Serializable]
    public class DungeonLayoutGeneratorConfig
    {
        public int RequiredRoomCellCount => _requiredCellCount;

        [SerializeField] private int _requiredCellCount;
    }
}