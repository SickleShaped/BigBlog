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
                   .HasMany(t => t.Tegs)
                   .WithMany(p => p.Articles)
                   .UsingEntity<ArticleTegEntity>(
                pt => pt
                    .HasOne(x => x.Teg)
                    .WithMany(x => x.ArticleTegEntities)
                    .HasForeignKey(x => x.TegId)
                    .OnDelete(DeleteBehavior.Cascade),
                pt => pt
                    .HasOne(a => a.Article)
                    .WithMany(at => at.ArticleTegEntities)
                    .HasForeignKey(a => a.ArticleId)
                    .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
