using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouYou.Business.Models;

namespace YouYou.Data.Mappings
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreationDate).IsRequired().HasColumnType("datetime");

            builder.Property(c => c.IsDeleted).IsRequired().HasColumnType("bit");

            builder.HasOne(f => f.JuridicalPerson);

            builder.HasOne(f => f.PhysicalPerson);

            builder.ToTable("Persons");
        }
    }
}
