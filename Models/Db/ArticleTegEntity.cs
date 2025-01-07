namespace BigBlog.Models.Db
{
    public class ArticleTegEntity
    {
        public Guid ArticleId { get; set; }
        public Guid TegId { get; set; }

        public Article Article { get; set; }
        public Teg Teg { get; set; }
    }
}
