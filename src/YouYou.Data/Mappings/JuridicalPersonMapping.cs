using YouYou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YouYou.Data.Mappings
{
    public class JuridicalPersonMapping : IEntityTypeConfiguration<JuridicalPerson>
    {
        public void Configure(EntityTypeBuilder<JuridicalPerson> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CNPJ)
                .HasColumnType("varchar(14)");

            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasColumnType("varchar(256)");

            builder.Property(c => c.TradingName)
                .HasColumnType("varchar(256)");

            builder.Property(c => c.FirstNumber)
               .IsRequired()
               .HasColumnType("varchar(256)");

            builder.Property(c => c.SecondNumber)
               .HasColumnType("varchar(256)");

            builder.Property(c => c.Email)
            .HasColumnType("varchar(256)");

            builder.ToTable("JuridicalPersons");
        }
    }
}
