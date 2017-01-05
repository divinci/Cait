using System;

namespace Cait.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static double AsUnixTimestamp(this DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static DateTime FromUnixTimestamp(this int timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).Add(new TimeSpan(0, 0, (int)timestamp));
        }
    }
}