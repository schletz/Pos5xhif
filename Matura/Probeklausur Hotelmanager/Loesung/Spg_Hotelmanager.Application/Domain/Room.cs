using System.Collections.Generic;

namespace Spg_Hotelmanager.Application.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string KeycardNumber { get; set; }
        public bool CanBeReserved { get; set; }
        public RoomCategory RoomCategory { get; set; }
        public decimal PricePerNight { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
