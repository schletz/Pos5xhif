using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2
{
    public class GradeService
    {
        public class ClassStatistics
        {

        }

        private readonly GradeContext _db;

        public GradeService(GradeContext db)
        {
            _db = db;
        }

        public ClassStatistics GetClassStatistics(string @class)
        {
            // TODO: Implementiere deine Servicemethode
            throw new NotImplementedException();
        }

        public bool TryAddRegistration(Student student, string subjectShortname, DateTime date)
        {
            // TODO: Implementiere deine Servicemethode
            throw new NotImplementedException();

        }
    }
}
