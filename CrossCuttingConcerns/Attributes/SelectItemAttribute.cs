using System;

namespace CrossCuttingConcerns.Attributes
{
    /// <summary>
    /// İlişkisel tablo gösteriminde çekilecek değeri imzalar
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectItemAttribute : Attribute
    {
        
    }
    
}