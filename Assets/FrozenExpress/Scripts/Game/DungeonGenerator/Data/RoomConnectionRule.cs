using UnityEngine;

namespace FrozenExpress.Game.DungeonGenerator
{
    [System.Serializable]
    public class RoomConnectionRule
    {
        public string SourceConnector => _sourceConnector;
        public string DestinationConnector => _destinationConnector;

        [SerializeField] private string _sourceConnector;
        [SerializeField] private string _destinationConnector;
    }
}