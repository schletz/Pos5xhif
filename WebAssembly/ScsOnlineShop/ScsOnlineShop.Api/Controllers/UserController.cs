using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScsOnlineShop.Api.Services;
using ScsOnlineShop.Shared.Dto;

namespace ScsOnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AuthService _authService;

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto login)
        {
            var (success, user) = _authService.TryLogin(login.Username, login.Password);
            if (!success) { return Unauthorized(); }
            return Ok(user);
        }
    }
}
