using System;

namespace FocusOnFlying.Application.Extensions
{
    public static class LongExtensions
    {
        public static DateTime ToLocalDateTime(this long unixTime)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).LocalDateTime;
        }

        public static DateTime? ToLocalDateTime(this long? unixTime)
        {
            return unixTime.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(unixTime.Value).LocalDateTime : default(DateTime?);
        }
    }
}
