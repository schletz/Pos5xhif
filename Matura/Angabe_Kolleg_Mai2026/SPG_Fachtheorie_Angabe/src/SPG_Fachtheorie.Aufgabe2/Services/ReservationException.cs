using System;

namespace SPG_Fachtheorie.Aufgabe2.Services;

[Serializable]
public class ReservationException : Exception
{
    public ReservationException()
    {
    }

    public ReservationException(string? message) : base(message)
    {
    }

    public ReservationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}