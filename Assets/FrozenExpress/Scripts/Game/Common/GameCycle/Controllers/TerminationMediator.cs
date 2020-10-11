using System;
using System.Collections.Generic;

namespace FrozenExpress.Game.Common.GameCycle
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
