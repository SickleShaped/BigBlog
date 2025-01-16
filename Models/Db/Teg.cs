namespace BigBlog.Models.Db
{
    public class Teg
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }

        public List<Article> Articles { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
