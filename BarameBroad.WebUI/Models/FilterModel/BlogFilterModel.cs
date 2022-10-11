namespace BarameBroad.WebUI.Models.FilterModel
{
    public class BlogFilterModel
    {
        public Guid? CId { get; set; }

        public string BlogCategoryFriendlyName { get; set; }

        public string BlogName { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 6;

    }
}
