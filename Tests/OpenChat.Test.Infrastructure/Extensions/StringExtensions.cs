using System;
using System.Globalization;

namespace OpenChat.Test.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ParseToFormat(this string dateTime, string format)
        {
            return DateTime.ParseExact(dateTime,
                format,
                CultureInfo.InvariantCulture);
        }
    }
}