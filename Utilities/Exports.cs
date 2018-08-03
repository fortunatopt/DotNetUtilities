using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Utilities
{
    public static class Exports
    {
        public static HttpResponseMessage BuildWorkbook<dynamic>(List<dynamic> data)
        {
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Export");

            worksheet.Columns().Width = 30;

            DataTable dt = data.ToDataTable<dynamic>();
            worksheet.Cell(1, 1).InsertTable(dt);

            if (dt != null)
            {
                for (int i = 1; i <= dt.Columns.Count; i++)
                {
                    worksheet.Cell(1, i).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Cell(1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Double;
                    worksheet.Cell(1, i).Style.Border.OutsideBorderColor = XLColor.Black;
                    worksheet.Cell(1, i).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(1, i).Style.Font.Bold = true;
                }
            }


            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var memoryStream = new MemoryStream(); // If I put this in a 'using' construct, I never get the response back in a browser.
            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin); // Seem to have to manually rewind stream before applying it to the content.
            response.Content = new StreamContent(memoryStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            string date = DateTime.Now.ToString("yyyy_MM_dd");
            string title = "Export_" + date + ".xlsx";
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = title };
            return response;
        }
        public static List<dynamic> DynamicData<T>(List<T> inputData, List<Column> columns)
        {
            List<dynamic> data = new List<dynamic>(); // Create dynamic list

            foreach (T listRow in inputData)
            {
                var row = new ExpandoObject() as IDictionary<string, Object>;

                foreach (Column column in columns)
                {
                    PropertyInfo listProp = listRow.GetType().GetProperty(column.Field);

                    if (listProp != null)
                        row.Add(column.Title, listProp.GetValue(listRow));

                }
                data.Add(row);
            }
            return data;
        }
        public static List<T> ConvertType<T>(List<object> list)
        {
            List<T> records = new List<T>();
            foreach (object o in list)
            {
                T r = JsonConvert.DeserializeObject<T>(o.ToString(), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });
                records.Add(r);
            }

            return records;
        }
        public class Column
        {
            public string Title { get; set; }
            public string Field { get; set; }
        }
    }
}
