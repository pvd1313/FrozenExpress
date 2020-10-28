using UnityEngine;

namespace FrozenExpress.Game.Dungeon
{
    [System.Serializable]
    public class DungeonLayoutGeneratorConfig
    {
        public int RequiredRoomCellCount => _requiredCellCount;

        [SerializeField] private int _requiredCellCount;
    }
}