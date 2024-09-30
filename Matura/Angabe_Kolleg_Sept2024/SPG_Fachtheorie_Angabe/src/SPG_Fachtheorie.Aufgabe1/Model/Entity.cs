namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public abstract class Entity<T> where T : struct
    {
        public T Id { get; private set; }
    }
}
