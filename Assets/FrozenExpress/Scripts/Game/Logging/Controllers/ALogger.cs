using System;
using System.Diagnostics;

namespace FrozenExpress.Scripts.Game.Logging.Controllers
{
    public abstract class ALogger
    {
        [Conditional("UNITY_DEBUG")]
        public abstract void Info(string message);

        public abstract void Error(string message);

        public abstract void Exception(Exception exception);

        public void Exception(Exception exception, string message)
        {
            Error(message);
            Exception(exception);
        }
    }
}
