using YouYou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YouYou.Data.Mappings
{
    public class PhysicalPersonMapping : IEntityTypeConfiguration<PhysicalPerson>
    {
        public void Configure(EntityTypeBuilder<PhysicalPerson> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CPF)
                .HasColumnType("varchar(11)");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(256)");

            builder.Property(c => c.FirstNumber)
               .IsRequired()
               .HasColumnType("varchar(256)");

            builder.Property(c => c.SecondNumber)
               .HasColumnType("varchar(256)");

            builder.Property(c => c.Email)
               .HasColumnType("varchar(256)");

            builder.Property(c => c.BirthdayDate).IsRequired(false).HasColumnType("datetime");

            builder.HasOne(c => c.Gender);

            builder.ToTable("PhysicalPersons");
        }
    }
}
