using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Dates
    {
        private static DateTime GetEpoch()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> kind.
        /// </summary>
        public static DateTime ToDateTimeFromEpoch(this long intDate) => GetEpoch().AddMilliseconds(intDate);

        /// <summary>
        /// Create Auth String
        /// </summary>
        /// <param name="first">true = first item in QueryString, false = not first item</param>
        /// <param name="guid">key to be used with the timestamp for the signature</param>
        /// <param name="privateKey">private key for the signature</param>
        /// <returns>string of Authentication Guid, Timestamp, and Signature</returns>
        public static string ToUnixTimestamp(this DateTime dateTime) => Math.Round(dateTime.Subtract(GetEpoch()).TotalMilliseconds).ToString();

        public static DateTime UnixTimeStampToDateTime(this string unixTimeStampString)
        {
            // Unix timestamp is seconds past epoch
            double unixTimeStamp = double.Parse(unixTimeStampString);
            System.DateTime dtDateTime = GetEpoch();
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static List<DateTime> SortAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static List<DateTime> SortDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        public static List<DateTime> SortMonthAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.Month.CompareTo(b.Month));
            return list;
        }

        public static List<DateTime> SortMonthDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.Month.CompareTo(a.Month));
            return list;
        }
    }
}
