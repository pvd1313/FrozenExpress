using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Game.GameCycle
{
    public class FrameUpdateMediator : ITerminatable
    {
        private readonly GameCycleMediatorView _view;
        private readonly List<IFrameUpdatable> _earlyUpdatables;
        private readonly List<IFrameUpdatable> _lateUpdatables;

        public FrameUpdateMediator(List<IFrameUpdatable> updatables, GameCycleMediatorView view)
        {
            _earlyUpdatables = updatables.Where(i => i.UpdateOrder < EFrameUpdateOrder.Late)
                                         .OrderBy(i => i.UpdateOrder)
                                         .ToList();

            _lateUpdatables = updatables.Where(i => i.UpdateOrder >= EFrameUpdateOrder.Late)
                                        .OrderBy(i => i.UpdateOrder)
                                        .ToList();

            _view = view;

            _view.OnUpdate += OnEarlyUpdate;
            _view.OnLateUpdate += OnLateUpdate;
        }

        private void OnEarlyUpdate()
        {
            var data = new FrameUpdateData()
            {
                DeltaTime = Time.deltaTime
            };

            foreach (var item in _earlyUpdatables)
                item.Update(data);
        }

        private void OnLateUpdate()
        {
            var data = new FrameUpdateData()
            {
                DeltaTime = Time.deltaTime
            };

            foreach (var item in _lateUpdatables)
                item.Update(data);
        }

        public void Terminate()
        {
            _view.OnUpdate -= OnEarlyUpdate;
            _view.OnLateUpdate -= OnLateUpdate;
        }
    }
}
