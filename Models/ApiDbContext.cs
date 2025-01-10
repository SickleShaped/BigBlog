using BigBlog.Models.Db;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace BigBlog.Models
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext() => Database.EnsureCreated();
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Comment> Comments{ get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Teg> Tegs { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

            builder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Пользователь",
                Description = "Обычный пользоватетль"},

            new Role
            {
                Id = 2,
                Name = "Модератор",
                Description = "Может редактировать чужие статьи и комментарии"},

            new Role
            {
                Id = 3,
                Name = "Администратор",
                Description = "Может делать всё" });

        }

    }
}
