using System.Collections.Generic;

namespace FrozenExpress.Game.DungeonLayout
{
    public class DungeonEntityDatabase : IDungeonEntityDatabase
    {
        private readonly DungeonEntityDatabaseAsset _asset;
        private readonly Dictionary<string, DungeonEntityConfig> _cash;

        private int _cashVersion;

        public DungeonEntityDatabase(DungeonEntityDatabaseAsset asset)
        {
            _cash = new Dictionary<string, DungeonEntityConfig>();
            _asset = asset;
            _cashVersion = -1;
        }

        public DungeonEntityConfig GetEntity(string id)
        {
            CashDatabase();

            if (!_cash.TryGetValue(id, out var item))
            {
                LogError($"Entity '{id}' was not found");
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
                    LogError($"Duplicated item id '{item.Id}' at index {i}");
                    continue;
                }

                _cash[item.Id] = item;
            }
        }

        private void LogError(string message)
        {
            UnityEngine.Debug.LogError($"{GetType().Name} :: {message}");
        }
    }
}
