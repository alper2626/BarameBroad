using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrossCuttingConcerns.Attributes;

namespace Extensions.UIExtensions
{
    public static class QueryStringBuilder
    {
        /// <summary>
        /// IFilterModel in Filterable olarak işaretlenmiş propertylerinden querystringe çevirir.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToQueryString(this object obj)
        {

            var properties = from p in obj.GetType().GetProperties().Where(q =>
                    q.CustomAttributes.Any(r => r.AttributeType == typeof(FilterableAttribute)))
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.HtmlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

    }
}