using BaseEntities.Concrete;
using BaseEntities.EnumTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseEntities.Database
{
    public class MasterContentDetail : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid RelatedId { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public EntityRelationType EntityRelationType { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<ContentDetail> Childrens { get; set; }
    }

    public class ContentDetail : LangEntity<MasterContentDetail>
    {
        public Guid RelatedId { get; set; }

        public EntityRelationType EntityRelationType { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Title { get; set; }

        public string FriendlyName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }

    public class ContentDetailTempModel
    {
        public string DisplayText { get; set; }

        public Guid Value { get; set; }

        public EntityRelationType EntityRelationType { get; set; }

    }

    public class ContentDetailGridParameter
    {
        public Guid Value { get; set; }

        public EntityRelationType EntityRelationType { get; set; }

    }

    public class MasterContentDetailWithTemp
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public MasterContentDetail MasterContentDetail { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public ContentDetailTempModel ContentDetailTempModel { get; set; }
    }
}
