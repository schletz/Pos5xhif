using System;

namespace SPG_Fachtheorie.Aufgabe1.Domain
{
    public class Test
    {
        public int Id { get; set; }
        public DateTime TestTimeStamp { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int StationId { get; set; }
        public TestStation Station { get; set; }
        public int? TestBay { get; set; }
        public TestKitType? TestKitType { get; set; }
        public TestResult? Result { get; set; }
    }
}
