using System;
using UnityEngine;

namespace FrozenExpress.Game.Serialization
{
    public abstract class AScriptableObjectDatabase : ScriptableObject
    {
        public event Action OnModify;

        private void OnValidate()
        {
            OnModify?.Invoke();
        }
    }
}