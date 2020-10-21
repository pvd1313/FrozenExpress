using FrozenExpress.Game.GameCycle;
using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonEntityDatabase : IDungeonEntityDatabase
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Database;

        private readonly DungeonEntityDatabaseAsset _asset;
        private readonly Dictionary<string, DungeonEntityConfig> _cash;
        private readonly ALogger _log;

        private int _cashVersion;

        public DungeonEntityDatabase(DungeonEntityDatabaseAsset asset,
                                     ALogFactory logFactory)
        {
            _cash = new Dictionary<string, DungeonEntityConfig>();
            _asset = asset;
            _cashVersion = -1;
            _log = logFactory.Create(this);
        }

        public DungeonEntityConfig GetEntity(string id)
        {
            CashDatabase();

            if (!_cash.TryGetValue(id, out var item))
            {
                _log?.Error($"'{id}' was not found");
                return null;
            }

            return item;
        }

        private void CashDatabase()
        {
            if (_cashVersion == _asset.RuntimeSerializedVersion)
            {
                return;
            }

            _cashVersion = _asset.RuntimeSerializedVersion;
            _cash.Clear();

            var items = _asset.Entities;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (_cash.ContainsKey(item.Id))
                {
                    _log?.Error($"Duplicated item id '{item.Id}' at index {i}");
                    continue;
                }

                _cash[item.Id] = item;
            }
        }
    }
}
