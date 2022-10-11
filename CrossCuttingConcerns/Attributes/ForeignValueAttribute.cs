using System;

namespace CrossCuttingConcerns.Attributes
{
    /// <summary>
    /// Hangi tablo ile ilişkili olduğunu gösterir
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignValueAttribute :Attribute
    {
        public string TableName { get;}

        public ForeignValueAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }
}