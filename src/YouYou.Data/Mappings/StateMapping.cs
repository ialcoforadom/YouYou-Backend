using YouYou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YouYou.Data.Mappings
{
    public class StateMapping : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(256)");

            builder.Property(c => c.UF)
                .IsRequired()
                .HasColumnType("char(2)");

            builder.HasMany(f => f.Cities)
                .WithOne(p => p.State)
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("States");
        }
    }
}
