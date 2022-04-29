using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe2.Helpers
{
    public static class HashExtensions
    {
        /// <summary>
        /// Generiert auf Basis des gespeicherten Secrets den Passworthash.
        /// </summary>
        public static string GenerateHash(string password, string secrret)
        {
            var secret = Convert.FromBase64String(secrret);
            var dataBytes = Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash = new System.Security.Cryptography.HMACSHA256(secret);
            byte[] hashedData = myHash.ComputeHash(dataBytes);
            return Convert.ToBase64String(hashedData);
        }

        /// <summary>
        /// Generiert eine 1024 Bit lange Zufallszahl und gibt sie Base64 codiert zurück.
        /// </summary>
        public static string GenerateSecret(int length)
        {
            byte[] secret = new byte[length / 8];
            using var rnd = System.Security.Cryptography.RandomNumberGenerator.Create();
            rnd.GetBytes(secret);
            return Convert.ToBase64String(secret);
        }
    }
}
