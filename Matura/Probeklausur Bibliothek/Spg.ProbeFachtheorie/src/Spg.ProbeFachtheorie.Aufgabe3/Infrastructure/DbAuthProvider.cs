using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using Spg.ProbeFachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.ProbeFachtheorie.Aufgabe3.Infrastructure
{
    public class DbAuthProvider
    {
        private readonly LibraryContext _dbContext;

        public DbAuthProvider(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (UserInfo userInfo, string errorMessage) CheckUser(string username, string password)
        {
            string message = string.Empty;

            User existingUser = _dbContext.Users.SingleOrDefault(u => u.UserName == username);
            if (existingUser != null)
            {
                UserRoles userRole = UserRoles.Guest;
                switch (existingUser.Role.ToString().ToUpper())
                {
                    case "BACKOFFICEEMPLOYEE":
                        userRole = UserRoles.BackOfficeEmployee;
                        break;
                }
                return (new UserInfo(existingUser.UserName, existingUser.EMail, existingUser.Id, userRole), message);
            }
            else
            {
                message = "Die Anmeldung ist fehlgeschlagen!";
            }
            return (null!, message);
        }
    }
}
