namespace ArticleService.Core.Models
{
    public class ArticleData
    {
        public int Id { get; set; }
        public string AssetId { get; set; }
        public string Slug { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string PublicationDateUtc { get; set; }
        public string Author { get; set; }
        public string AssetType { get; set; }
        public string Body { get; set; }
        public string Geographies { get; set; }
        public string Topics { get; set; }
        public int Version { get; set; }
    }
}
