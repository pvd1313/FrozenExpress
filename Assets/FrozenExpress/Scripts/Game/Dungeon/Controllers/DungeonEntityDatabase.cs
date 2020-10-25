using System.Collections.Generic;
using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Dungeon.Assets;
using FrozenExpress.Scripts.Game.Dungeon.Data;
using FrozenExpress.Scripts.Game.Dungeon.Interfaces;
using FrozenExpress.Scripts.Game.GameCycle.Enums;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;

namespace FrozenExpress.Scripts.Game.Dungeon.Controllers
{
    public class DungeonEntityDatabase : IDungeonEntityDatabase, IInitializable, ITerminatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Database;

        private readonly DungeonEntityDatabaseAsset _asset;
        private readonly Dictionary<string, DungeonEntityConfig> _cash;
        private readonly ADebugLogger _debugLog;

        public DungeonEntityDatabase(DungeonEntityDatabaseAsset asset,
                                     ADebugFactory debugFactory)
        {
            _cash = new Dictionary<string, DungeonEntityConfig>();
            _asset = asset;
            _debugLog = debugFactory.CreateLogger(this);

            _asset.OnModified += CashDatabase;
        }
        
        public void Initialize()
        {
            CashDatabase();
        }

        public DungeonEntityConfig GetEntity(string id)
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
        }

        public void Terminate()
        {
            _asset.OnModified -= CashDatabase;
        }
    }
}
