using System.ComponentModel;

namespace BaseEntities.EnumTypes
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin,
        [Description("Personel")]
        Staff,
        [Description("Müşteri")]
        Customer
    }
}