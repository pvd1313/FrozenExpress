using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using FrozenExpress.Scripts.Game.GameCycle.Data;
using FrozenExpress.Scripts.Game.GameCycle.Enums;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;
using FrozenExpress.Scripts.Game.GameCycle.Views;
using UnityEngine;
using UnityEngine.Playables;

namespace FrozenExpress.Scripts.Game.GameCycle.Controllers
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
            FrameUpdateData data;
            data.DeltaTime = Time.deltaTime;
            data.UnscaledTime = Time.unscaledTime;
            
            foreach (var item in _earlyUpdatables)
                item.Update(data);
        }

        private void OnLateUpdate()
        {
            FrameUpdateData data;
            data.DeltaTime = Time.deltaTime;
            data.UnscaledTime = Time.unscaledTime;

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
