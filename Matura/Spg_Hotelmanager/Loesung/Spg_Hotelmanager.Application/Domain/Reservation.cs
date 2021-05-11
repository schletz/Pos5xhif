using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg_Hotelmanager.Application.Domain
{
    [Table("Reservation")]
    public class Reservation
    {
        public int Id { get; set; } 
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public decimal? InvoiceAmount { get; set; }
    }
}
