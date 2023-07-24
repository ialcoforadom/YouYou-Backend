using YouYou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YouYou.Data.Mappings
{
    public class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsCompany)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.Disabled)
                .HasDefaultValue(true)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnType("bit");

            builder.HasMany(f => f.UserRoles)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.PhysicalPerson)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.JuridicalPerson)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
