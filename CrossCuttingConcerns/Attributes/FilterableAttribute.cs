using System;

namespace CrossCuttingConcerns.Attributes
{
    /// <summary>
    /// FilterModelden Türeyen nesnelerin işaretlenmiş propertylerinden querystring oluşturmaya yarar
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterableAttribute : Attribute
    {
        public FilterableAttribute()
        {
        }
    }
}