using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ScsOnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocaleTestController : ControllerBase
    {
        [HttpGet("date")]
        public IActionResult GetDate()
        {
            return Ok(DateTime.Now.ToString());
        }
        [HttpGet("number/{value}")]
        public IActionResult GetNumber(decimal value)
        {
            return Ok(value.ToString());
        }
        // POST https://localhost:7120/api/localetest/number
        [HttpPost("number")]
        public IActionResult PostNumber([FromForm] decimal value)
        {
            return Ok(value.ToString());
        }
    }
}
