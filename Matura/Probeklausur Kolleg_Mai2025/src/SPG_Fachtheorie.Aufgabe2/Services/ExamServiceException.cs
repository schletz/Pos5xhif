using System;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    [Serializable]
    public class ExamServiceException : Exception
    {
        public ExamServiceException()
        {
        }

        public ExamServiceException(string? message) : base(message)
        {
        }

        public ExamServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}