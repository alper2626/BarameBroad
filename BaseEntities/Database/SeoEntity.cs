using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseEntities.Database
{
    [Description("Master Seo")]
    public class MasterSeoEntity : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string TableName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid RelatedId { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<SeoEntity> Childrens { get; set; }
    }

    [Description("Seo")]
    public class SeoEntity : LangEntity<MasterSeoEntity>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Keywords { get; set; }
    }
}