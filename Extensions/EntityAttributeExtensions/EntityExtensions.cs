using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using BaseEntities.Abstract;
using CrossCuttingConcerns.Attributes;

namespace Extensions.EntityAttributeExtensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Tablonun görüntülenebilir adını verir adını verir.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetDescription(this IEntity entity)
        {
            var descriptions = (DescriptionAttribute[])
                entity.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions.Length == 0)
                return "";

            return descriptions[0].Description;
        }

        /// <summary>
        /// Görünür durumdaki property info listesini verir
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetDisplayProperties(this Type type)
        {
            return type.GetProperties()
                .Where(w =>
                    w.CustomAttributes.Any(t => t.AttributeType == typeof(PropertyNameAttribute))).ToList();
        }

        /// <summary>
        /// Entitynin SelectItemAttribute imzasını tasıyan propertynin name ini döndürür
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string SelectedItemValue(this IEntity entity)
        {
            return typeof(IEntity).GetProperties()
                .FirstOrDefault(w =>
                    w.CustomAttributes.Any(t => t.AttributeType == typeof(SelectItemAttribute)))?.Name;
        }

    }
}