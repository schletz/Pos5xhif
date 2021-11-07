using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScsOnlineShop.Shared.Dto
{
    /// <summary>
    /// DTO Klasse für Stores.
    /// Da wir auch neue Stores erstellen, darf das kein immutable object sein
    /// (das Form setzt den Wert des Namens auf den Inhalt des Formularfeldes)
    /// </summary>
    public class StoreDto
    {
        public StoreDto(Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }

        public Guid Guid { get; }

        [Required(ErrorMessage = "Fehlender Name")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Ungültiger Name")]
        public string Name { get; set; }
    }
}