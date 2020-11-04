using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    [System.Serializable]
    public class DungeonLayoutGeneratorConfig
    {
        public int RequiredRoomCellCount => _requiredCellCount;
        public int RequiredExitPointCount => _requiredExitPointCount;
        public IReadOnlyList<RoomConnectionRule> ConnectionRules => _connectionRules;

        [SerializeField] 
        private int _requiredCellCount;
        
        [SerializeField] 
        private int _requiredExitPointCount;
        
        [SerializeField, OneLine]
        private List<RoomConnectionRule> _connectionRules;
    }
}