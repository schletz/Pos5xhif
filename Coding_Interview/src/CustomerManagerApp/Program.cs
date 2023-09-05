using System;
using CustomerManagerApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var customerManager = new CustomerManager();
        try
        {
            customerManager.RegisterCustomer("first", "last", new DateTime(2022, 1, 1));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        customerManager.RegisterCustomer("first", "last", new DateTime(2000, 1, 1));
        var maxDiscount = customerManager.GetMaxDiscount("first", "last", 2000);
        Console.WriteLine("Max Discount for first last: " + maxDiscount);

        customerManager.RegisterCustomer("first", "last", new DateTime(2002, 1, 1));
        Console.WriteLine("Number of Customers: " + customerManager.Customers.Count);
    }
}
