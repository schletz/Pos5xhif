using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using Spg.ProbeFachtheorie.Aufgabe2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BorrowCharge> BorrowCharges => Set<BorrowCharge>();
        public DbSet<PickupTime> PickupTimes => Set<PickupTime>();
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();
        public DbSet<BorrowChargeType> BorrowChargeTypes => Set<BorrowChargeType>();
        public DbSet<User> Users => Set<User>();

        public LibraryContext(DbContextOptions options)
            : base(options)
        { }

        public void Seed()
        {

            Randomizer.Seed = new Random(091007);

            var borrowChargeTypes = new List<BorrowChargeType>()
            {
                new BorrowChargeType(){ Name="Normalpreis"},
                new BorrowChargeType(){ Name="Ermäßigt"},
            };
            BorrowChargeTypes.AddRange(borrowChargeTypes);
            SaveChanges();



            var books = new Faker<Book>("de").Rules((f, e) =>
            {
                e.Ean13 = f.Commerce.Ean13();
                e.Name = f.Commerce.ProductName();
                e.Abstract = f.Lorem.Paragraph();

                //e.Category = f.Random.ListItem(categories);
            })
            .Generate(200)
            .ToList();

            Books.AddRange(books);
            SaveChanges();



            var borrowCharge = new Faker<BorrowCharge>("de").Rules((f, e) =>
            {
                e.Amount = f.Finance.Amount();
                e.Currency = "EUR";

                e.Book = f.Random.ListItem(books);
                e.BorrowChargeType = f.Random.ListItem(borrowChargeTypes);
            })
            .Generate(400)
            .ToList();

            BorrowCharges.AddRange(borrowCharge);
            SaveChanges();



            var users = new Faker<User>("de").Rules((f, e) =>
            {
                e.UserName = "";
                e.EMail = f.Internet.Email();
                e.FirstName = f.Name.FirstName();
                e.LastName = f.Name.LastName();
                e.Salt = HashExtensions.GenerateSecret(128);
                e.PasswordHash = HashExtensions.GenerateHash("geheim", e.Salt);
                e.Role = f.Random.Enum<UserRoles>();
                e.Guid = Guid.NewGuid();
            })
            .Generate(500)
            .ToList();

            Users.AddRange(users);
            SaveChanges();

            int userindex = 0;
            foreach (User user in Users)
            {
                user.UserName = $"user{userindex++}";
                Update(user);
            }
            SaveChanges();



            string[] cartNames = new string[] { "Vorlesebücher", "Nachlesebücher", "Andere Bücher", "Gute Nachtgeschichten", "Peppa Pig", "Märchenbücher", "Liederbücher", "Puzzlebücher", "Kinderreime", "Bilderbücher", "Kriminalromane", "Abenteuergeschichten", "Science Fiction Romane", "Gruselgeschichten", "Kochbücher", "Grillrezepte" };
            var shoppingCarts = new Faker<ShoppingCart>("de").Rules((f, e) =>
            {
                e.Name = f.Random.ArrayElement(cartNames);
                e.User = f.Random.ListItem(users);
                e.Guid = Guid.NewGuid();
            })
            .Generate(25)
            .ToList();

            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();



            var shoppingCartitems = new Faker<ShoppingCartItem>("de").Rules((f, e) =>
            {
                e.Book = f.Random.ListItem(books);
                e.ShoppingCart = f.Random.ListItem(shoppingCarts);
                e.ShoppingCartItemState = ShoppingCartItemStates.Reserved;
            })
            .Generate(100)
            .ToList();

            ShoppingCartItems.AddRange(shoppingCartitems);
            SaveChanges();
        }
    }
}
