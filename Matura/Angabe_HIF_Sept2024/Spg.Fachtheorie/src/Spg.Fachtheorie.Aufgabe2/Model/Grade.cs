using System.Text.Json.Serialization;

namespace Spg.Fachtheorie.Aufgabe2.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SubjectId { get; set; }
        [JsonIgnore()]
        public Subject SubjectNavigation { get; set; } = default!;

        // TODO: Add further properties as specified
    }
}
