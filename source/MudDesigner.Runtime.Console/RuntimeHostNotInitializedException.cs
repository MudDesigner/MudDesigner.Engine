using System;
using System.Runtime.Serialization;

namespace MudDesigner.Runtime.ConsoleApp
{
    [Serializable]
    internal class RuntimeHostNotInitializedException : Exception
    {
        public RuntimeHostNotInitializedException()
        {
        }

        public RuntimeHostNotInitializedException(string message) : base(message)
        {
        }

        public RuntimeHostNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RuntimeHostNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}