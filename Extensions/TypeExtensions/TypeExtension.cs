using System;

namespace Extensions.TypeExtensions
{
    public static class TypeExtension
    {
        public static bool IsGuid(object obj)
        {
            if (obj == null)
                return false;
            return Guid.TryParse(obj.ToString(), out Guid g);
        }

    }
}