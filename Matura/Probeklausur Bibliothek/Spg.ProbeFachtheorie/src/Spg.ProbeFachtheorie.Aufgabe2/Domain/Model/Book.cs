using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model
{
    public class Book : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public string Ean13 { get; set; } = string.Empty;


        public BorrowCharge BorrowCharges { get; set; } = default!;
    }
}
