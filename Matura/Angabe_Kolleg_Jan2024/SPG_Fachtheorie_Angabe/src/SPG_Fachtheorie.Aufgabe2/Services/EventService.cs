using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record ContingentStatistics(int SoldTickets, int ReservedTickets, Show Show);

    public class EventService
    {
        private readonly EventContext _db;

        public EventService(EventContext db)
        {
            _db = db;
        }

        public ContingentStatistics CalcContingentStatistics(int contingentId)
        {
            throw new NotImplementedException();
        }

        public int CreateReservation(int guestId, int contingentId, int pax, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}