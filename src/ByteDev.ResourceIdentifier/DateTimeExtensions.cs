using System;
using System.Text;

namespace ByteDev.ResourceIdentifier
{
    internal static class DateTimeExtensions
    {
        public static int YearTwoDigits(this DateTime source)
        {
            return Convert.ToInt32(source.ToString("yy"));
        }

        public static string ToBase64String(this DateTime source, string format)
        {
            var dt = source.ToString(format);

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(dt));
        }
    }
}