using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("Store")]
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
