using System;

namespace CrossCuttingConcerns.Attributes
{
    /// <summary>
    /// Property nin Label değerini gösterir
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNameAttribute : Attribute
    {
        public string Description { get; }
        public PropertyNameAttribute(string description)
        {
            this.Description = description;
        }
    }
}