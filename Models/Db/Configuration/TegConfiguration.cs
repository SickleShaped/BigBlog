using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBlog.Models.Db.Configuration
{
    public class TegConfiguration : IEntityTypeConfiguration<Teg>
    {
        public void Configure(EntityTypeBuilder<Teg> builder)
        {
            builder.ToTable("Tegs");
            builder.HasKey(k => k.Id);

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Tegs)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
