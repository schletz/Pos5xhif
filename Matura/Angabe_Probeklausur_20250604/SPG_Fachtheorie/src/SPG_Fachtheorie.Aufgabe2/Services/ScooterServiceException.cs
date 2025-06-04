using System;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    [Serializable]
    public class ScooterServiceException : Exception
    {
        public ScooterServiceException()
        {
        }

        public ScooterServiceException(string? message) : base(message)
        {
        }

        public ScooterServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}