using System.Text.Json.Serialization;

namespace Spg.Fachtheorie.Aufgabe2.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Seats { get; set; }
        public Grade Grade { get; set; } = default!;
        [JsonIgnore()]
        public Student StudentNavigation { get; set; } = default!;

        // TODO: Add further properties as specified
    }
}
