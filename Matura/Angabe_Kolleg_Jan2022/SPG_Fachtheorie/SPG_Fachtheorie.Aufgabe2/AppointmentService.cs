using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2
{
    public class AppointmentService
    {
        private readonly AppointmentContext _db;

        public AppointmentService(AppointmentContext db)
        {
            _db = db;
        }

        public bool AskForAppointment(Guid offerId, Guid studentId, DateTime date)
        {
            // TOTO: Implementiere die Methode
            return default;
        }

        public bool ConfirmAppointment(Guid appointmentId)
        {
            // TOTO: Implementiere die Methode
            return default;
        }

        public bool CancelAppointment(Guid appointmentId)
        {
            // TOTO: Implementiere die Methode
            return default;
        }
    }
}
