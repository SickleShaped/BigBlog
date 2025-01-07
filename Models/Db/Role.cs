namespace BigBlog.Models.Db
{
    public class Role
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<User> Users { get; set; }
    }
}
