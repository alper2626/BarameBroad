using System.ComponentModel;

namespace BaseEntities.EnumTypes
{
    public enum OperationType
    {
        [Description("Kayıt")]
        Create,
        [Description("Güncelleme")]
        Update,
        [Description("Silme")]
        Delete,
        [Description("Hata")]
        Error,
        [Description("Giriş")]
        Login,
        [Description("Kritik Hata")]
        CriticalError,
    }
}