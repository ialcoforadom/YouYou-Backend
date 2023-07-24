using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouYou.Business.Models;

namespace YouYou.Data.Mappings
{
    public class DeviceHistoryMapping : IEntityTypeConfiguration<DeviceHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceHistory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.BindingDate).IsRequired().HasColumnType("datetime");

            builder.Property(c => c.UnbindingDate).IsRequired(false).HasColumnType("datetime");

            builder.HasOne(f => f.User);

            builder.HasOne(f => f.Device);

            builder.HasOne(f => f.Person);

            builder.Property(c => c.Status).IsRequired().HasColumnType("bit").HasDefaultValue(true);

            builder.ToTable("DeviceHistories");
        }
    }
}
