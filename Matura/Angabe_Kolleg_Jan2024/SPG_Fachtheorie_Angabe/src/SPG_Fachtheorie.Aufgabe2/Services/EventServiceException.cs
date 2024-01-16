using System;
using System.Runtime.Serialization;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    [Serializable]
    public class EventServiceException : Exception
    {
        public EventServiceException()
        {
        }

        public EventServiceException(string? message) : base(message)
        {
        }

        public EventServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EventServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}