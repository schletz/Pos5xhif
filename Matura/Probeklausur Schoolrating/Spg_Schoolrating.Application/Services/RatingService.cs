using Spg_Schoolrating.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg_Schoolrating.Application.Services
{
    public class RatingService
    {
        private readonly RatingContext _db;

        public RatingService(RatingContext db)
        {
            _db = db;
        }


        /// <summary>
        /// TODO: Tausche object durch den Typ SchoolStat
        /// </summary>
        public object GetSchoolStatistics(int schoolId)
        {
            return 0;
        }


    }
}
