using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
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
                var content = stream.ReadToEnd().AsSpan();

                while (true)
                {
                    int begin = content.IndexOf("INSERT INTO");
                    if (begin == -1) { break; }
                    bool inQuotation = false;
                    int end = -1;
                    for (int i = begin; i < content.Length; i++)
                    {
                        char readChar = content[i];
                        if (readChar == '\'') { inQuotation = !inQuotation; }
                        if (readChar == ';' && !inQuotation) { end = i; break; }
                    }
                    if (end == -1) { break; }
                    var statement = content.Slice(begin, end - begin + 1);
                    db.Database.ExecuteSqlCommand(statement.ToString());
                    content = content.Slice(end + 1);
                }
            }
        }
    }

}
