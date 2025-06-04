using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record TripDto(int Id, DateTime Begin, DateTime? End, int ScooterId, string ScooterModel, int UserId, string UserEmail, bool IsParked);
    /// <summary>
    /// Verwende die Testklasse in Aufgabe2.Test/ScooterServiceTests, um deine Implementierung zu testen.
    /// </summary>
    public class ScooterService
    {
        private readonly ScooterContext _db;
        public ScooterService(ScooterContext db)
        {
            _db = db;
        }

        public Trip AddTrip(int userId, int scooterId, DateTime begin)
        {
            throw new NotImplementedException();
        }

        public List<TripDto> GetTripInfos(DateTime beginFrom, DateTime beginTo)
        {
            throw new NotImplementedException();
        }

        public int CalculateTripLength(int tripId)
        {
            throw new NotImplementedException();
        }

        public decimal CalculatePrice(int tripId)
        {
            throw new NotImplementedException();
        }
    }
}
