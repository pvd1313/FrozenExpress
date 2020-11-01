using System;
using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.GameCycle;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonEntityDatabase : IDungeonEntityDatabase, IInitializable, ITerminatable
    {
        public event Action OnCashUpdate;
        
        public EInitializationOrder InitializationOrder => EInitializationOrder.Database;

        public IEnumerable<DungeonEntity> All => _cash.Values;

        private readonly DungeonEntityDatabaseAsset _asset;
        private readonly Dictionary<string, DungeonEntity> _cash;
        private readonly ADebugLogger _debugLog;

        public DungeonEntityDatabase(DungeonEntityDatabaseAsset asset,
                                     ADebugFactory debugFactory)
        {
            _cash = new Dictionary<string, DungeonEntity>();
            _asset = asset;
            _debugLog = debugFactory.CreateLogger(this);

            _asset.OnModify += CashDatabase;
        }
        
        public void Initialize()
        {
            CashDatabase();
        }

        public DungeonEntity GetEntity(string id)
        {
            if (!_cash.TryGetValue(id, out var item))
            {
                _debugLog?.Error($"'{id}' was not found");
                return null;
            }

            return item;
        }

        private void CashDatabase()
        {
            _cash.Clear();

            var items = _asset.Entities;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (_cash.ContainsKey(item.Id))
                {
                    _debugLog?.Error($"Duplicated item id '{item.Id}' at index {i}");
                    continue;
                }

                _cash[item.Id] = item;
            }
            
            OnCashUpdate?.Invoke();
        }

        public void Terminate()
        {
            _asset.OnModify -= CashDatabase;
        }
    }
}
