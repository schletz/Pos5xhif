using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg_Schoolrating.Application.Infrastructure;
using Spg_Schoolrating.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg_Schoolrating.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly RatingService _service;
        private readonly RatingContext _db;

        public TestController(RatingService service, RatingContext db)
        {
            _service = service;
            _db = db;
        }

        [HttpGet("SchoolStatistics/{schoolId}")]
        public IActionResult GetSchoolStatistics(int schoolId)
        {
            return Ok(_service.GetSchoolStatistics(schoolId));
        }

    }
}
