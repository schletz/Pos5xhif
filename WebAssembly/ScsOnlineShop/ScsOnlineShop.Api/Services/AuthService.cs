using Microsoft.IdentityModel.Tokens; // Nuget System.IdentityModel.Tokens.Jwt
using ScsOnlineShop.Shared.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ScsOnlineShop.Api.Services
{
    public class AuthService
    {
        private readonly byte[] _secret;
        private readonly bool _isDevelopment;

        public AuthService(string secret, bool isDevelopment)
        {
            _secret = Convert.FromBase64String(secret);
            _isDevelopment = isDevelopment;

        }
        public (bool success, UserDto? user) TryLogin(string username, string password)
        {
            // TODO: Echte Passwortprüfung gegen die Datenbank, ... einfügen und je nach
            //       Bedarf _isDevelopment prüfen, um jedes Passwort im Development Mode zu erlauben.
            if (!_isDevelopment) { return (false, null); }
            // TODO: Rolle entsprechend des Benutzers setzen.
            var role = "admin";

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // Payload für den JWT.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Benutzername als Typ ClaimTypes.Name.
                    new Claim(ClaimTypes.Name, username),
                    // Rolle des Benutzer als ClaimTypes.DefaultRoleClaimType setzen.
                    // ACHTUNG: Muss mit den Annotations der Routen in [Authorize(Roles = "xxx")]
                    // übereinstimmen.
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return (true, new UserDto(
                Username: username, 
                Role: role, 
                Token: tokenHandler.WriteToken(token)));
        }
    }
}
