using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg_Fahrzeugvermietung.Application.Domain
{

    public class Fahrzeug
    {
        public int Id { get; set; }
        public string Kennzeichen { get; set; }
        public int Kilometerstand { get; set; }
        public Lenkberechtigung? Lenkberechtigung { get; set; }
        public decimal Tagesmiete { get; set; }
        public decimal PreisProKm { get; set; }
    }
}
