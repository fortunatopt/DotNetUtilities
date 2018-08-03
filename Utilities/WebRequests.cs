using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Utilities
{
    public static class WebRequests
    {
        public static string postJSON(this string url, object postObject)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json; charset=utf-8";
            request.Method = "POST";

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(postObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        public static string postFORMEncoded(this string url, string formPostData)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(formPostData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Get data service.
        /// </summary>
        /// <param name="url">Service url</param>
        /// <returns>Returns response as a string</returns>
        public static string getJSON(this string url)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.Accept = "application/json; charset=utf-8";
            request.Method = "GET";

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Get data service.
        /// </summary>
        /// <param name="url">Service url</param>
        /// <returns>Returns response as a string</returns>
        public static string getJSONWithIp(this string url, string ip)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.Accept = "application/json; charset=utf-8";
            request.Method = "GET";
            request.Headers.Add("True-Client-IP", ip);

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Get data service.
        /// </summary>
        /// <param name="url">Service url</param>
        /// <returns>Returns response as a string</returns>
        public static string getJSONNoContType(this string url)
        {
            var webAddr = url;
            var request = (HttpWebRequest)WebRequest.Create(webAddr);
            request.Accept = "application/json; charset=utf-8";
            request.Method = "GET";

            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}
