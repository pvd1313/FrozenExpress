using System.Collections.Generic;
using System.Linq;

namespace FrozenExpress.Game.DungeonGenerator
{
    public partial class DungeonLayoutGenerator
    {
        private class RoomConnectionController
        {
            private readonly Dictionary<string, HashSet<string>> _connectorMap;
            private readonly Dictionary<string, IList<BakedRoom>> _allowedRoomsByConnector;
            
            public RoomConnectionController()
            {
                _connectorMap = new Dictionary<string, HashSet<string>>();
                _allowedRoomsByConnector = new Dictionary<string, IList<BakedRoom>>();
            }

            public void AllowConnection(string connectorA, string connectorB)
            {
                if (!_connectorMap.TryGetValue(connectorA, out var map))
                {
                    map = new HashSet<string>();
                    _connectorMap[connectorA] = map;
                }
            
                map.Add(connectorB);
            }

            public void Clear()
            {
                foreach (var hash in _connectorMap.Values)
                {
                    hash.Clear();
                }

                foreach (var rooms in _allowedRoomsByConnector.Values)
                {
                    rooms.Clear();
                }
            }
            
            public void AddRoom(BakedRoom room)
            {
                var connectorType = room.Source.ConnectorType;
                
                foreach (var kvp in _connectorMap)
                {
                    if (!kvp.Value.Contains(connectorType))
                    {
                        continue;
                    }

                    if (!_allowedRoomsByConnector.TryGetValue(kvp.Key, out var rooms))
                    {
                        rooms = new List<BakedRoom>();
                        _allowedRoomsByConnector[kvp.Key] = rooms;
                    }
                
                    rooms.Add(room);
                }
            }

            public IEnumerable<BakedRoom> FindAvailableRooms(string sourceConnector)
            {
                if (_allowedRoomsByConnector.TryGetValue(sourceConnector, out var result))
                {
                    return result;
                }
                
                return Enumerable.Empty<BakedRoom>();
            }
        }
    }
}