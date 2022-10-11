using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;

namespace BaseEntities.Database
{
    [Description("Master Blog")]
    public class MasterBlog : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid BlogCategoryId { get; set; }

        [ForeignKey("BlogCategoryId")]
        public virtual BlogCategory BlogCategory { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<BlogEntity> Childrens { get; set; }
    }

    [Description("Bloglar")]
    public class BlogEntity : LangEntity<MasterBlog>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }

}