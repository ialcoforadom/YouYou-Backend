using Microsoft.EntityFrameworkCore;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Data.Context;

namespace YouYou.Data.Repository
{
    public class LogExceptionRepository: ILogExceptionRepository
    {
        private readonly YouYouDbContext Db;
        private readonly DbSet<LogException> DbSet;

        public LogExceptionRepository(YouYouDbContext db)
        {
            Db = db;
            DbSet = db.Set<LogException>();
        }

        public async Task Add(LogException logException)
        {
            DbSet.Add(logException);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
