using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Spg_Fahrzeugvermietung.Application.Infrastructure
{
    public static class DbContextExtensions
    {
        public static void Import(this DbContext db, string filename)
        {
            var path = AppContext.BaseDirectory;
            while (!File.Exists(Path.Combine(path, filename)))
            {
                path = Directory.GetParent(path).FullName;
            }
            using (var stream = new StreamReader(Path.Combine(path, filename), Encoding.UTF8))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        db.Database.ExecuteSqlCommand(line);
                    }
                }
            }
        }
    }
}
