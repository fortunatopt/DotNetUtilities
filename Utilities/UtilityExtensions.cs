using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Utilities
{
    public static class UtilityExtensions
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;

            var dt = new DataTable();

            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                dt.Columns.Add(key);
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }
        /// <summary>
        /// Method for getting HttpStatusCode from WebException
        /// </summary>
        /// <param name="we">Object of type WebException</param>
        /// <returns>Object of type HttpStatusCode or 0</returns>
        public static HttpStatusCode GetHttpStatusCode(this WebException we)
        {
            if (we.Response is HttpWebResponse)
            {
                HttpWebResponse response = (HttpWebResponse)we.Response;
                return response.StatusCode;
            }
            return 0;
        }
        public static string GetServerIP(this string criteria)
        {

            string host = Dns.GetHostName();
            string ip = null;

            for (int i = 0; i <= Dns.GetHostEntry(host).AddressList.Length - 1; i++)
            {
                if (criteria == null)
                {
                    ip = Dns.GetHostEntry(host).AddressList[i].MapToIPv4().ToString();
                }
                else
                {

                    if (Dns.GetHostEntry(host).AddressList[i].MapToIPv4().ToString().StartsWith(criteria) == true)
                        return Dns.GetHostEntry(host).AddressList[i].MapToIPv4().ToString();
                }

            }
            return ip;

        }
        public static List<string> ToStringList(this string value, char separator) => value.Split(separator).OfType<string>().ToList();
        public static string StripCharacter(this string variable, string characters) => variable.Replace(characters, string.Empty);
        public static string ToSnakeCase(this string str) => str.ToLower().Replace(' ', '_');
        /// <summary>
        /// Method for converting a string of integers into an array of longs
        /// </summary>
        /// <param name="value">String of Value to be parsed</param>
        /// <param name="separator">Character of Separator Character</param>
        /// <returns>Array of Long</returns>
        public static long[] ToLongArray(this string value, char separator)
        {
            value = value.Replace(" ", null);
            string[] array = null;

            try
            {
                array = value.Split(separator);
            }
            catch
            {
                throw;
            }

            long[] longArray = null;

            try
            {
                longArray = Array.ConvertAll(array, s => long.Parse(s));
            }
            catch
            {
                throw;
            }

            return longArray;

        }
        /// <summary>
        /// Method for converting a string of integers into an array of integers
        /// </summary>
        /// <param name="value">String of Value to be parsed</param>
        /// <param name="separator">Character of Separator Character</param>
        /// <returns>Array of Integers</returns>
        public static int[] ToIntArray(this string value, char separator) => Array.ConvertAll(value.Split(separator), s => int.Parse(s));
        /// <summary>
        /// Method for converting a string to an int
        /// </summary>
        /// <param name="input">String of Input to be converted</param>
        /// <returns>Integer</returns>
        public static int StringToInt(this string input)
        {
            int output = int.TryParse(input, out output) ? output : 0;

            return output;
        }
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
