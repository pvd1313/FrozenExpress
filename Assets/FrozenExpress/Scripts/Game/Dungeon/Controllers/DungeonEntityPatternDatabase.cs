using System;
using System.Collections.Generic;
using FrozenExpress.Game.GameCycle;

namespace FrozenExpress.Game.Dungeon
{
    public class DungeonEntityPatternDatabase : IDungeonEntityPatternDatabase, ITerminatable, IInitializable
    {
        public event Action OnCashUpdate;

        public IEnumerable<EntityPattern> All => _asset.Patterns;
        public EInitializationOrder InitializationOrder => EInitializationOrder.Database;

        private readonly DungeonEntityPatternDatabaseAsset _asset;


        public DungeonEntityPatternDatabase(DungeonEntityPatternDatabaseAsset asset)
        {
            _asset = asset;

            _asset.OnModify += CashDatabase;
        }

        public void Initialize()
        {
            CashDatabase();
        }

        private void CashDatabase()
        {
            OnCashUpdate?.Invoke();
        }

        public void Terminate()
        {
            _asset.OnModify -= CashDatabase;
        }
    }
}