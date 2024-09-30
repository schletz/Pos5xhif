using System;
using System.Runtime.Serialization;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    [Serializable]
    public class LanguageweekServiceException : Exception
    {
        public LanguageweekServiceException()
        {
        }

        public LanguageweekServiceException(string? message) : base(message)
        {
        }

        public LanguageweekServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}