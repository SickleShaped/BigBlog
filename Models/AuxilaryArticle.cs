using BigBlog.Models.Db;

namespace BigBlog.Models
{
    public class AuxilaryArticle
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public Teg Teg { get; set; }
        public Guid TegId { get; set; }

        public List<Teg> PossibleTegs { get; set; }
        public string TegCountNumber { get; set; }
    }
}
