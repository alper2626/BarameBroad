using System;
using BaseEntities.Concrete;

namespace BaseEntities.Poco
{
    public class GetBlogModel : GetModel
    {
        public Guid MasterId { get; set; }

        public Guid BlogCategoryId { get; set; }

        public string BlogCategoryName { get; set; }

        public string Name { get; set; }

        public string FriendlyName { get; set; }
    }
}