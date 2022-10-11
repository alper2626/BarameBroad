using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace BaseEntities.EnumTypes
{
    public enum FormItemStatus
    {
        [Description("Değişmedi")]
        NotChange,
        [Description("Önceden Eklenmiş")]
        AddedBefore,
        [Description("Silindi")]
        Deleted,
        [Description("Oluşturuldu")]
        Created,
        [Description("Güncellendi")]
        Updated
    }

    public static class FormItemConverter
    {
        public static EntityState ToEntityState(this FormItemStatus status)
        {
            switch (status)
            {
                case FormItemStatus.Deleted:
                    return EntityState.Deleted;
                case FormItemStatus.Created:
                    return EntityState.Added;
                case FormItemStatus.Updated:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}