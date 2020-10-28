using System;
using FrozenExpress.Game.Debugging;
using Debug = UnityEngine.Debug;

namespace FrozenExpress.Game.Debugging
{
    public class UnityDebugLogger : ADebugLogger
    {
        private string _ownerName;

        public UnityDebugLogger(object owner)
        {
            _ownerName = owner.GetType().Name;
        }

        public override void Error(string message)
        {
            message = FormatMessage(message);
            Debug.LogError(message);
        }

        public override void Exception(Exception exception)
        {
            Debug.LogException(exception);
        }

        public override void Info(string message)
        {
            message = FormatMessage(message);
            Debug.Log(message);
        }

        private string FormatMessage(string message)
        {
            return $"{_ownerName} :: {message}";
        }
    }
}
