using System;
using System.Collections.Generic;
using BaseEntities.Abstract;
using BaseEntities.EnumTypes;
using Microsoft.EntityFrameworkCore;

namespace BaseEntities.Concrete
{
    public class CreateModel : ICreateModel
    {
        private Guid _id;
        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                    _id = Guid.NewGuid();
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public DateTime CreateTime => DateTime.Now;
    }

    public class UpdateModel : IUpdateModel
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime => DateTime.Now;
    }


    /// <summary>
    /// Status bilgisi create ise Id Propertysi ile ilişkilendirilecek verinin Idsi geçirilir.
    /// Diğer durumlarda ( Update, Delete ) ilişkinin kendi Id verisi tutulur.
    /// </summary>
    public class FormItemStatusModel : UpdateModel
    {
        public string DisplayName { get; set; }

        public FormItemStatus OldStatus { get; set; }

        public FormItemStatus NewStatus { get; set; }

        public EntityState State => NewStatus.ToEntityState();
    }

    

}