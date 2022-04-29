using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Services
{
    public class PickupTimeService
    {
        private readonly LibraryContext _db;

        public PickupTimeService(LibraryContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Mustermethode für einen Unittest.
        /// </summary>
        public IQueryable<Book> GetAllBooks()
        {
            return _db.Books;
        }
    }
}
