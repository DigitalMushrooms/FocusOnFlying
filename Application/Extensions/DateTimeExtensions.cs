using System;

namespace FocusOnFlying.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToLocalTime().ToUnixTimeMilliseconds();
        }

        public static long? ToUnixTime(this DateTime? dateTime)
        {
            return dateTime.HasValue ? new DateTimeOffset(dateTime.Value).ToLocalTime().ToUnixTimeMilliseconds() : default(long?);
        }
    }
}
