namespace DataLayer.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
