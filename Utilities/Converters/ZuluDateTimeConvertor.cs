using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Utilities
{
    /// <summary>
    /// Class for Zulu Date Time Converter
    /// </summary>
    public class ZuluDateTimeConvertor : DateTimeConverterBase
    {
        /// <summary>
        /// Method to Read Json Data
        /// </summary>
        /// <param name="reader">Object of type JsonReader</param>
        /// <param name="objectType">Object of type Type</param>
        /// <param name="existingValue">Object</param>
        /// <param name="serializer">Object of type JsonSerializer</param>
        /// <returns>Object</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                DateTime dtTest = new DateTime();
                bool dateTest = DateTime.TryParse(reader.Value.ToString(), out dtTest);
                if (dateTest == true)
                {
                    return (DateTime)reader.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Method for Write Json
        /// </summary>
        /// <param name="writer">Object of type JsonWriter</param>
        /// <param name="value">Object</param>
        /// <param name="serializer">JsonSerializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string date = ((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            writer.WriteValue(date);
        }
    }
}
