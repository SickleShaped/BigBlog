using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBlog.Models.Db.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(k => k.Id);

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p=>p.Teg)
                .WithMany(u => u.Articles)
                .HasForeignKey(p => p.TegId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
