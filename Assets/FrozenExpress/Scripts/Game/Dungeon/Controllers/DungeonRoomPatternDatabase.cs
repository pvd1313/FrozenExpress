using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.GameCycle;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonRoomPatternDatabase : IDungeonRoomPatternDatabase, ITerminatable, IInitializable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Database;

        public IEnumerable<RoomPattern> All => _cash.Values;

        private readonly Dictionary<string, RoomPattern> _cash;
        private readonly DungeonRoomPatternDatabaseAsset _asset;
        private readonly ADebugLogger _debugLog;
        
        public DungeonRoomPatternDatabase(DungeonRoomPatternDatabaseAsset asset,
                                          ADebugFactory debugFactory)
        {
            _asset = asset;
            _debugLog = debugFactory.CreateLogger(this);
            _cash = new Dictionary<string, RoomPattern>();

            asset.OnModified += CashDatabase;
        }
        
        public void Initialize()
        {
            CashDatabase();
        }

        private void CashDatabase()
        {
            _cash.Clear();
            
            var items = _asset.Patterns;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (_cash.ContainsKey(item.Id))
                {
                    _debugLog?.Error($"Duplicated item id '{item.Id} at index {i}");
                    continue;
                }

                _cash[item.Id] = item;
            }
        }

        public void Terminate()
        {
            _asset.OnModified -= CashDatabase;
        }
    }
}