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
        [HttpGet("SchoolBySchoolNr/{schoolNumber}")]
        public IActionResult GetSchoolBySchoolNr(int schoolNumber)
        {
            return Ok(_db.GetSchoolBySchoolNr(schoolNumber));
        }
        [HttpGet("TeachersBySchool/{schoolId}")]
        public IActionResult GetTeachersBySchool(int schoolId)
        {
            return Ok(_db.GetTeachersBySchool(schoolId));
        }
        [HttpGet("AverageTeacherRating/{teacherId}/{categoryId}")]
        public IActionResult GetAverageTeacherRating(int teacherId, int categoryId)
        {
            return Ok(_db.GetAverageTeacherRating(teacherId, categoryId));
        }

        [HttpGet("AverageTeacherRating/{teacherId}/{categoryId}/{ratedFrom}")]
        public IActionResult GetAverageTeacherRating(int teacherId, int categoryId, DateTime ratedFrom)
        {
            return Ok(_db.GetAverageTeacherRating(teacherId, categoryId, ratedFrom));
        }

    }
}
