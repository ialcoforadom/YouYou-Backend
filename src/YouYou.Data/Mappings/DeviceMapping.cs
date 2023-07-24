using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouYou.Business.Models;

namespace Servis.Data.Mappings
{
    public class DeviceMapping : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasColumnType("varchar(45)");

            builder.Property(c => c.Code)
                .IsRequired()
                .HasColumnType("varchar(256)");

            builder.Property(c => c.IsDeleted).IsRequired().HasColumnType("bit");

            builder.HasOne(f => f.Person);

            builder.Property(c => c.IsActive).IsRequired().HasColumnType("bit").HasDefaultValue(true);

            builder.ToTable("Devices");
        }
    }
}
