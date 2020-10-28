using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class DungeonLayoutGeneratorConfig
    {
        public int RequiredRoomCellCount => _requiredCellCount;
        public int RequiredExitPointCount => _requiredExitPointCount;

        [SerializeField] private int _requiredCellCount;
        [SerializeField] private int _requiredExitPointCount;
    }
}