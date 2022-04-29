using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Model.Custom;
using Spg.ProbeFachtheorie.Aufgabe3.Exceptions;
using Spg.ProbeFachtheorie.Aufgabe3.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe3.Services
{
    public class HttpCookieAuthService
    {
        private readonly HttpContext _httpContext;
        private readonly DbAuthProvider _authProvider;

        public HttpCookieAuthService(
            DbAuthProvider authProvider,
            IHttpContextAccessor httpContext)
        {
            _authProvider = authProvider;
            _httpContext = httpContext.HttpContext;
        }

        public async Task Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or empty", nameof(username));
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty", nameof(password));
            }

            var (userInfo, errorMessage) = _authProvider.CheckUser(username, password);
            if (string.IsNullOrEmpty(errorMessage))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.UserName),
                    new Claim(nameof(User), JsonSerializer.Serialize(userInfo)),
                    new Claim(ClaimTypes.Role, userInfo.UserRole.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
                };

                await _httpContext.SignInAsync(
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            else
            {
                throw new AuthServiceException(errorMessage);
            }
        }

        public Task Logout()
        {
            return _httpContext.SignOutAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated
            => _httpContext.User.Identity.IsAuthenticated;

        public string Username()
            => _httpContext.User.Identity.Name;

        public int UserId()
        {
            string userInfoString = _httpContext.User.Claims.SingleOrDefault(c => c.Type == nameof(User))?.Value;
            if (!string.IsNullOrEmpty(userInfoString))
            {
                UserInfo userInfo = JsonSerializer.Deserialize<UserInfo>(userInfoString);
                return userInfo?.UserId 
                    ?? throw new AuthServiceException("User kann nicht zugeordnet werden!");
            }
            throw new AuthServiceException("User kann nicht zugeordnet werden!");
        }

        public bool HasRole(string role)
            => _httpContext.User.IsInRole(role.ToString());
    }
}
