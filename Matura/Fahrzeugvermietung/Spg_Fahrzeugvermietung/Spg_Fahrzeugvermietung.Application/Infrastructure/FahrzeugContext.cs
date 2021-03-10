using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg_Fahrzeugvermietung.Application.Infrastructure
{
    public class FahrzeugContext : DbContext
    {

        public FahrzeugContext() : base() { }
        public FahrzeugContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Fahrzeug.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        // METHODE ZUM ERZEUGEN DER MUSTERDATEN. IST ZU IGNORIEREN.
        //public void Seed()
        //{
        //    string[] bl = { "W", "N", "B" };
        //    int[] sitzpl = { 2, 5 };
        //    Randomizer.Seed = new Random(1117);
        //    List<Fahrzeug> fahrzeuge = new List<Fahrzeug>();

        //    fahrzeuge.AddRange(new Faker<Pkw>("de").Rules((f, p) =>
        //    {
        //        p.Kennzeichen = f.Random.ListItem(bl) + f.Random.Int(10000, 99999) + f.Random.String2(2).ToUpper();
        //        p.Lenkberechtigung = null;
        //        p.PreisProKm = Math.Round(f.Random.Decimal(1, 2), 2);
        //        p.Tagesmiete = Math.Round(f.Random.Decimal(40, 100));
        //        p.AnzahlSitzplaetze = f.Random.ListItem(sitzpl);
        //        p.Kilometerstand = f.Random.Int(1000, 99999);
        //    })
        //    .Generate(5)
        //    .ToList());

        //    fahrzeuge.AddRange(new Faker<Transporter>("de").Rules((f, t) =>
        //    {
        //        t.Kennzeichen = f.Random.ListItem(bl) + f.Random.Int(10000, 99999) + f.Random.String2(2).ToUpper();
        //        t.PreisProKm = Math.Round(f.Random.Decimal(1, 2));
        //        t.Tagesmiete = Math.Round(f.Random.Decimal(30, 50));
        //        t.MaxNutzlast = f.Random.Int(1000, 5000);
        //        t.Gesamtgewicht = t.MaxNutzlast + f.Random.Int(800, 1200);
        //        t.Lenkberechtigung = t.Gesamtgewicht < 3500 ? default(Lenkberechtigung?) : Lenkberechtigung.Lkw;
        //        t.Kilometerstand = f.Random.Int(1000, 199999);
        //    })
        //    .Generate(5)
        //    .ToList());

        //    fahrzeuge.AddRange(new Faker<Motorrad>("de").Rules((f, m) =>
        //    {
        //        m.HubraumCcm = f.Random.Bool() ? 125 : f.Random.Int(200, 1200);
        //        m.Kennzeichen = f.Random.ListItem(bl) + f.Random.Int(10000, 99999) + f.Random.String2(2).ToUpper();
        //        m.PreisProKm = Math.Round(f.Random.Decimal(1, 2));
        //        m.Tagesmiete = Math.Round(f.Random.Decimal(20, 40));
        //        m.Lenkberechtigung = m.HubraumCcm <= 125 ? default(Lenkberechtigung?) : Lenkberechtigung.Motorrad;
        //        m.Kilometerstand = f.Random.Int(1000, 99999);
        //    })
        //    .Generate(5)
        //    .ToList());

        //    fahrzeuge.AddRange(new Faker<EBike>("de").Rules((f, e) =>
        //    {
        //        e.MotorleistungWatt = f.Random.Int(200, 400);
        //        e.PreisProKm = Math.Round(f.Random.Decimal(1, 2));
        //        e.Tagesmiete = Math.Round(f.Random.Decimal(5, 15));
        //        e.Lenkberechtigung = e.MotorleistungWatt < 250 ? Lenkberechtigung.Ohne : Lenkberechtigung.Moped;
        //        e.Kennzeichen = e.MotorleistungWatt < 250 ? default : f.Random.ListItem(bl) + f.Random.Int(10000, 99999) + f.Random.String2(2).ToUpper();
        //        e.Kilometerstand = f.Random.Int(10, 4999);
        //    })
        //    .Generate(5)
        //    .ToList());

        //    var kunden = new Faker<Kunde>("de").Rules((f, k) =>
        //    {
        //        k.Name = new Name { Vorname = f.Person.FirstName, Nachname = f.Person.LastName };
        //        k.Email = k.Name.Nachname.ToLower() + "@" + f.Internet.DomainName().ToLower();
        //        k.FuehrerscheinNr = f.Random.String2(1).ToUpper() + f.Random.String2(6, "0123456789") + f.Random.String2(1).ToUpper();
        //        for (int i = 0; i < f.Random.Int(0, 5); i++)
        //        {
        //            var verliehenAb = new DateTime(f.Date.Between(
        //                new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        //                new DateTime(2021, 4, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
        //            DateTime verliehenBis = verliehenAb.AddDays(f.Random.Int(1, 6));

        //            k.Vermietungen.Add(new Vermietung
        //            {
        //                Fahrzeug = f.Random.ListItem(fahrzeuge),
        //                VerliehenAb = verliehenAb,
        //                VerliehenBis = verliehenBis,
        //                Rechnungsbetrag = verliehenBis != null ? Math.Round(f.Random.Decimal(30, 600)) : default(decimal?)
        //            });
        //        }
        //    })
        //    .Generate(10)
        //    .ToList();

        //    Fahrzeuge.AddRange(fahrzeuge);
        //    Kunden.AddRange(kunden);
        //    SaveChanges();
        //}
    }
}
