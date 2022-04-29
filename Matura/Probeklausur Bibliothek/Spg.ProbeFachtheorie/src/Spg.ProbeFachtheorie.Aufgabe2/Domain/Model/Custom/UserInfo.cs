using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom
{
    public enum UserRoles { Guest, BackOfficeEmployee }

    public record UserInfo(
        string UserName,
        string EMail,
        int UserId,
        UserRoles UserRole
        );
}
