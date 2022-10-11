using System.Collections.Generic;
using System.Reflection;
using CrossCuttingConcerns.Attributes;

namespace Extensions.PropertyAttributeExtensions
{
    public static class PropertyExtensions
    {

        /// <summary>
        /// Property infolardan propertynameattribute değerlerini bulup döndürür.
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        public static List<string> GetPropertiesDisplayNames(this List<PropertyInfo> props)
        {
            var strs = new List<string>();
            foreach (var selectedProp in props)
            {
                var attrs = (PropertyNameAttribute[])selectedProp.GetCustomAttributes(typeof(PropertyNameAttribute), false);
                if (attrs.Length > 0)
                    strs.Add(attrs[0].Description);
            }
            return strs;
        }

        /// <summary>
        /// ForeignValue Attribute değerini döndürür.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static string GetForeignTable(this PropertyInfo prop)
        {
            var attrs = (ForeignValueAttribute[])prop.GetCustomAttributes(typeof(ForeignValueAttribute), false);
            if (attrs.Length > 0)
                return $"{attrs[0].TableName}";

            return "";
        }


    }
}