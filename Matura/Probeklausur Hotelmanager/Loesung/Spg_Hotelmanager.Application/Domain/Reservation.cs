using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg_Hotelmanager.Application.Domain
{
    /// <summary>
    /// 1 Customer hat n Reservations
    /// </summary>
    [Table("Reservation")]
    public class Reservation
    {
        public int Id { get; set; }  // ID --> PK, int Id --> AUTOINCREMENT PK
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        // FK Wert des Customers
        // 1) int, da Customer ein int als PK hat.
        // 2) Name = Property (Customer) + PK Name (Id)
        public int CustomerId { get; set; }        // NOT NULL
        public Customer Customer { get; set; }
        public int EmployeeId { get; set; }        // NOT NULL
        public Employee Employee { get; set; }
        public int? RoomId { get; set; }          // NULL
        public Room Room { get; set; }
        public decimal? InvoiceAmount { get; set; }   // NULL
    }
}
