using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
