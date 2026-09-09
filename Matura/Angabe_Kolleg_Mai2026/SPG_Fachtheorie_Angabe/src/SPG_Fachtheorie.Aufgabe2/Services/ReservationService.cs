using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services;

public record CategoryWithCount(string Text, int Count);

public class ReservationService
{
    private readonly ReservationContext _db;
    public ReservationService(ReservationContext db)
    {
        _db = db;
    }

    public List<CategoryWithCount> GetCategoriesWithCount()
    {
        // TODO: Add your implementation
        throw new NotImplementedException();
    }

    public List<Person> GetPersonsWithOpenReservations()
    {
        // TODO: Add your implementation
        throw new NotImplementedException();
    }

    public List<InventoryItem> GetInventoryItemsWithoutReservations(int year)
    {
        // TODO: Add your implementation
        throw new NotImplementedException();
    }

    public Reservation AddReservation(int personId, int inventoryItemId, DateOnly loanDate)
    {
        // TODO: Add your implementation
        throw new NotImplementedException();
    }
}
