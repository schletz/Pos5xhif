using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services
{

    public record LanguageweekStatisticsDto(
        int Id, DateTime From, DateTime To, string DestinationCity, string TeacherEmail,
        int Participants, int StudentsInClass);

    public class LanguageweekService
    {
        private readonly LanguageweekContext _db;

        public LanguageweekService(LanguageweekContext db)
        {
            _db = db;
        }

        public List<LanguageweekStatisticsDto> CalculateStatistics()
        {
            // TODO: Remove exception and add implementation
            throw new NotImplementedException();
        }
        public void AssignSupportTeacher(int languageweekId, int supportTeacherId)
        {
            // TODO: Remove exception and add implementation
            throw new NotImplementedException();
        }
    }
}