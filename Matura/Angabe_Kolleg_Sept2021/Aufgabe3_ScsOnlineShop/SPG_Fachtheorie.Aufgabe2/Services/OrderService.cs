using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public class OrderService
    {
        private readonly StoreContext _db;

        public OrderService(StoreContext db)
        {
            _db = db;
        }
    }
}
