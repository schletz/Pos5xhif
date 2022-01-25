using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Domain
{
    public class TestStation
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public List<Test> Tests { get; set; }
    }
}
