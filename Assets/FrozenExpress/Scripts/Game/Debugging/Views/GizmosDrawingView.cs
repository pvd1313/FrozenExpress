using System;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Views
{
    public class GizmosDrawingView : MonoBehaviour
    {
        public event Action OnGizmosDraw;

        private void OnDrawGizmos()
        {
            OnGizmosDraw?.Invoke();
        }
    }
}