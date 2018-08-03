using Kendo.DynamicLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities
{
    public static class KendoConverter
    {
        public class KendoObjects
        {
            public Filter KendoFilter { get; set; }
            public List<Sort> KendoSort { get; set; }
        }

        public static List<Sort> ConvertSort(this string sort)
        {
            return sort != null ? JsonConvert.DeserializeObject<List<Sort>>(sort) : null;
        }
        public static Filter ConvertFilter<T>(this string filter)
        {
            Filter kf = filter != null ? JsonConvert.DeserializeObject<Filter>(filter) : null;
            if (kf != null)
                SetFilterType<T>(kf);

            return kf;
        }

        public static KendoObjects ConvertKendo<T>(string filter, string sort)
        {
            Filter kf = filter != null ? JsonConvert.DeserializeObject<Filter>(filter) : null;
            if (kf != null)
                SetFilterType<T>(kf);

            List<Sort> ks = sort != null ? JsonConvert.DeserializeObject<List<Sort>>(sort) : null;

            return new KendoObjects() { KendoFilter = kf, KendoSort = ks };

        }

        public static void SetFilterType<T>(Filter KendoFilters)
        {
            Filter filt = new Filter();
            IEnumerable<Filter> filters = KendoFilters.Filters;

            if (KendoFilters.Operator != null && KendoFilters.Value != null)
            {
                KendoFilters.Value = SetTypedValue<T>(KendoFilters.Field, KendoFilters.Value);
            }

            foreach (Filter filter in filters)
            {
                if (filter.Filters == null)
                {
                    filter.Value = SetTypedValue<T>(filter.Field, filter.Value); ;
                }
                else
                {
                    SetFilterType<T>(filter);
                }
            }

        }

        public static object SetTypedValue<T>(string queryField, object value)
        {
            Type t = typeof(T);

            Type itemType = t.GetProperty(queryField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;

            var underlyingType = Nullable.GetUnderlyingType(itemType);

            if (underlyingType == null)
                return Convert.ChangeType(value, itemType);

            return String.IsNullOrEmpty(value.ToString()) ? null : Convert.ChangeType(value, underlyingType);

        }
    }
}
