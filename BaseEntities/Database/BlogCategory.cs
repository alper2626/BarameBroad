using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseEntities.Database
{
    [Description("Master Blog Kategorileri")]
    public class MasterBlogCategory : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public List<BlogCategory> Childrens { get; set; }
    }

    [Description("Blog Kategorileri")]
    public class BlogCategory : LangEntity<MasterBlogCategory>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public int BlogCount { get; set; }

        public virtual ICollection<BlogEntity> Blogs { get; set; }
    }

}