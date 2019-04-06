using System;
using System.Runtime.Serialization;

namespace MudDesigner.Runtime.Console
{
    [Serializable]
    internal class DisposingUndeletedHostException : Exception
    {
        public DisposingUndeletedHostException()
        {
        }

        public DisposingUndeletedHostException(string message) : base(message)
        {
        }

        public DisposingUndeletedHostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DisposingUndeletedHostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}