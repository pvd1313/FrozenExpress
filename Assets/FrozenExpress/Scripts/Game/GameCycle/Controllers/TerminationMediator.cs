using System;
using System.Collections.Generic;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;

namespace FrozenExpress.Scripts.Game.GameCycle.Controllers
{
    public class TerminationMediator : IDisposable
    {
        private readonly List<ITerminatable> _finalizables;

        public TerminationMediator([Zenject.InjectLocal] List<ITerminatable> finalizables)
        {
            _finalizables = finalizables;
        }

        public void Dispose()
        {
            foreach (var item in _finalizables)
                item.Terminate();
        }
    }
}
