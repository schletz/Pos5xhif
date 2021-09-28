using System;
using System.Collections.Generic;
using System.Text;

namespace Spg_Schoolrating.Application.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static DateTime SecondsAccurate(this DateTime dateTime) =>
            new DateTime(dateTime.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond, dateTime.Kind);
    }
}
