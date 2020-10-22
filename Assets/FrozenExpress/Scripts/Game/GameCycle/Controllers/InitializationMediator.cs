using System.Collections.Generic;
using System.Linq;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;

namespace FrozenExpress.Scripts.Game.GameCycle.Controllers
{
    public class InitializationMediator : Zenject.IInitializable
    {
        private readonly List<IInitializable> _initializables;

        public InitializationMediator([Zenject.InjectLocal] List<IInitializable> initializables)
        {
            _initializables = initializables;
        }

        public void Initialize()
        {
            var entities = _initializables.OrderBy(i => i.InitializationOrder);
            foreach (var item in entities)
                item.Initialize();
        }
    }
}
