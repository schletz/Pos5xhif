using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SPG_Fachtheorie.Aufgabe2.Model;

public class Staff
{
    protected Staff() { }
    [SetsRequiredMembers]
    public Staff(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [JsonIgnore]
    public List<Reservation> Reservations { get; } = new();
}
public class Person
{
    protected Person() { }
    [SetsRequiredMembers]
    public Person(string firstName, string lastName, string schoolClass, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        SchoolClass = schoolClass;
        Email = email;
        Phone = phone;
    }

    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string SchoolClass { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    [JsonIgnore]
    public List<Reservation> Reservations { get; } = new();
}

public class Category
{
    protected Category() { }
    [SetsRequiredMembers]
    public Category(string text)
    {
        Text = text;
    }
    public int Id { get; set; }
    public required string Text { get; set; }
    [JsonIgnore]
    public List<InventoryItem> IntentoryItems { get; } = new();
}
public class Status
{
    [SetsRequiredMembers]
    public Status(string text)
    {
        Text = text;
    }

    public int Id { get; set; }
    public required string Text { get; set; }
    [JsonIgnore]
    public List<Reservation> Reservations { get; } = new();
}

public class InventoryItem
{
    protected InventoryItem() { }
    [SetsRequiredMembers]
    public InventoryItem(string serialNumber, string itemName, Category category, bool isAvailable)
    {
        SerialNumber = serialNumber;
        ItemName = itemName;
        Category = category;
        IsAvailable = isAvailable;
    }

    public int Id { get; set; }
    public required string SerialNumber { get; set; }
    public required string ItemName { get; set; }
    public required Category Category { get; set; }
    public required bool IsAvailable { get; set; }
    [JsonIgnore]
    public List<Reservation> Reservations { get; } = new();

}
public class Reservation
{
    protected Reservation() { }
    [SetsRequiredMembers]
    public Reservation(
        Person person, InventoryItem inventoryItem, DateOnly loanDate,
        Status status, DateOnly? returnDate = null, Staff? acceptedBy = null)
    {
        Person = person;
        InventoryItem = inventoryItem;
        LoanDate = loanDate;
        Status = status;
        ReturnDate = returnDate;
        AcceptedBy = acceptedBy;
    }
    public int Id { get; set; }
    public required Person Person { get; set; }
    public required InventoryItem InventoryItem { get; set; }
    public required DateOnly LoanDate { get; set; }
    public required Status Status { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public Staff? AcceptedBy { get; set; }
}
