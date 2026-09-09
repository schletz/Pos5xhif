namespace SPG_Fachtheorie.Aufgabe3.Dtos;

public record ReservationDto(int Id, DateOnly LoanDate, DateOnly? ReturnDate, string PersonFirstName, string PersonLastName);