namespace Spg.Fachtheorie.Aufgabe2.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Subject> Subjects { get; set; } = new();

        // TODO: Add further properties as specified
    }
}
