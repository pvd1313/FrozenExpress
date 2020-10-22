using System;
using Debug = UnityEngine.Debug;

namespace FrozenExpress.Scripts.Game.Logging.Controllers
{
    public class UnityLogger : ALogger
    {
        private string _ownerName;

        public UnityLogger(object owner)
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
