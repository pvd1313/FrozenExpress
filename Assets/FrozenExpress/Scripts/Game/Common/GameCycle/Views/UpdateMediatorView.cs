using System;
using UnityEngine;

namespace FrozenExpress.Game.Common.GameCycle
{
    public class UpdateMediatorView : MonoBehaviour
    {
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;

        private void Update() => OnUpdate?.Invoke();

        private void LateUpdate() => OnLateUpdate?.Invoke();

        private void FixedUpdate() => OnFixedUpdate?.Invoke();
    }
}
