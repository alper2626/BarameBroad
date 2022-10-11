namespace BaseEntities.Concrete
{
    public class SiteMapEntity
    {
        public string Data { get; set; }

        public string Url => $"https://monibolittlebaby.com/{this.Data}";

    }
}