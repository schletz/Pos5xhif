using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe3RazorPages.Services
{
    public class AuthService
    {
        private readonly StoreContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(StoreContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public int CurrentStore => int.TryParse(_httpContextAccessor.HttpContext.User.Identity.Name, out var id) ? id : 0;
        public async Task Login(int storeId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, storeId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(6),
            };
            _db.Stores.FirstOrDefault(s => s.Id == storeId);

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task Logout() => await _httpContextAccessor.HttpContext.SignOutAsync();
    }
}
