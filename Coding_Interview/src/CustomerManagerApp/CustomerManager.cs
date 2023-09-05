using System;
using System.Collections.Generic;
using System.IO;

namespace CustomerManagerApp
{
    public class Customer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class CustomerManager
    {
        public List<Customer> Customers = new List<Customer>();

        public void RegisterCustomer(string firstname, string lastname, DateTime dateOfBirth)
        {
            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                if ((int)(DateTime.Now - dateOfBirth).TotalDays / 365 < 18)
                {
                    throw new Exception("A customer must be 18 years old");
                }
                Customer customer = new Customer();
                customer.Firstname = firstname;
                customer.Lastname = lastname;
                customer.DateOfBirth = dateOfBirth;
                customer.Age = (int)(DateTime.Now - dateOfBirth).TotalDays / 365;
                customer.RegistrationDate = DateTime.Now;
                for (int i = 0; i < Customers.Count; i++)
                {
                    if (Customers[i].Firstname + " " + Customers[i].Lastname == firstname + " " + lastname)
                        return;
                }
                Customers.Add(customer);
                File.AppendAllText("customerlog.txt", "Registration of Customer" + customer.Firstname + " " + customer.Lastname);
            }
        }

        public List<Customer> GetLoyalCustomers()
        {
            List<Customer> ret = new List<Customer>();
            for (int i = 0; i < Customers.Count; i++)
            {
                if ((DateTime.Now - Customers[i].RegistrationDate).TotalDays > 365)
                    ret.Add(Customers[i]);
            }
            return ret;
        }

        public List<Customer> GetVeryLoyalCustomers()
        {
            List<Customer> ret = new List<Customer>();
            for (int i = 0; i < Customers.Count; i++)
            {
                if ((DateTime.Now - Customers[i].RegistrationDate).TotalDays > 2 * 365)
                    ret.Add(Customers[i]);
            }
            return ret;
        }

        public decimal GetMaxDiscount(string firstname, string lastname, decimal amount)
        {
            var veryLoyalCustomers = GetVeryLoyalCustomers();
            var loyalCustomers = GetLoyalCustomers();

            decimal maxDiscount = 0;
            for (int i = 0; i < veryLoyalCustomers.Count; i++)
            {
                if (veryLoyalCustomers[i].Firstname + " " + veryLoyalCustomers[i].Lastname == firstname + " " + lastname)
                {
                    if (amount > 1000) maxDiscount = 30;
                    else maxDiscount = amount * 0.03M;
                    return maxDiscount;
                }
            }
            for (int i = 0; i < loyalCustomers.Count; i++)
            {
                if (loyalCustomers[i].Firstname + " " + loyalCustomers[i].Lastname == firstname + " " + lastname)
                {
                    if (amount > 1000) maxDiscount = 20;
                    else maxDiscount = amount * 0.02M;
                    return maxDiscount;
                }
            }
            if (amount > 1000) maxDiscount = 10;
            else maxDiscount = amount * 0.01M;
            return maxDiscount;
        }
    }
}