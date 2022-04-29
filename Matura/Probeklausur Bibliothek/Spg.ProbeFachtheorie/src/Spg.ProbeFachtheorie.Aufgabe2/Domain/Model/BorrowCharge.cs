using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model
{
    public class BorrowCharge : EntityBase
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;


        public int BookId { get; set; }
        public Book Book { get; set; } = default!;
        public int BorrowChargeTypeId { get; set; }
        public BorrowChargeType BorrowChargeType { get; set; } = default!;
    }
}
