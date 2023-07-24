using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace YouYou.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly YouYouDbContext Db;
        protected readonly DbSet<ApplicationUser> DbSet;

        public UserRepository(YouYouDbContext db)
        {
            Db = db;
            DbSet = db.Set<ApplicationUser>();
        }

        public async Task<ApplicationUser> GetByIdWithPerson(Guid id)
        {
            return await Db.ApplicationUsers.AsNoTracking()
                .Include(c => c.PhysicalPerson)
                .Include(c => c.JuridicalPerson)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
