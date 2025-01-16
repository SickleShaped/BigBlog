using BigBlog.Models.Db;

namespace BigBlog.Models
{
    public class AuxinaryUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public List<Comment> Comments { get; set; }
        public List<Article> Articles { get; set; }
        public List<Teg> Tegs { get; set; }
        public Role Role { get; set; }
        public uint RoleId { get; set; }

        public List<Role> PossibleRoles { get; set; }
        public string RoleName { get; set; }
    }
}
