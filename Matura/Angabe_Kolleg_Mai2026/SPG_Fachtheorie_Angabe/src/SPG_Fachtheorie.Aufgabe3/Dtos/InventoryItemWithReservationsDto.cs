namespace SPG_Fachtheorie.Aufgabe3.Dtos;

public record InventoryItemWithReservationsDto(int Id, string SerialNumber, string ItemName, List<ReservationDto> Reservations);
