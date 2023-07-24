using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YouYou.Business.Models;

namespace YouYou.Data.Context
{
    public class YouYouDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public YouYouDbContext(DbContextOptions<YouYouDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<JuridicalPerson> JuridicalPersons { get; set; }
        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YouYouDbContext).Assembly);

            #region Filtro Deletado

            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(p => !p.IsDeleted);

            #endregion

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            modelBuilder.Entity<ApplicationUser>().HasIndex(x => x.NormalizedUserName)
               .IsUnique(false)
               .HasName("UserNameIndex");
            modelBuilder.Entity<ApplicationUser>().Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

        }
    }
}
