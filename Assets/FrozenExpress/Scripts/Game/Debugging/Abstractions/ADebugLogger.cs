using System;
using System.Diagnostics;

namespace FrozenExpress.Game.Debugging
{
    public abstract class ADebugLogger
    {
        [Conditional("DEBUG")]
        public abstract void Info(string message);

        [Conditional("DEBUG")]
        public abstract void Error(string message);

        [Conditional("DEBUG")]
        public abstract void Exception(Exception exception);

        [Conditional("DEBUG")]
        public void Exception(Exception exception, string message)
        {
            Error(message);
            Exception(exception);
        }
    }
}
