using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Data.Context;

namespace YouYou.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly YouYouDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(YouYouDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
        }

        public virtual async Task<TEntity> GetByIdTracked(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            await SaveChanges();
        }

        public async Task UpdateKey(TEntity entity, object key)
        {
            TEntity exist = Db.Set<TEntity>().Find(key);

            if (exist != null)
            {
                Db.Entry(exist).CurrentValues.SetValues(entity);
            }

            await SaveChanges();
        }

        public virtual async Task Remove(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task RemoveEntity(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public async Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = DbSet.AsQueryable();
            query.Where(predicate);
            return await query.ToListAsync();
        }
        public async Task<bool> CheckExist(int id)
        {
            return await DbSet.AnyAsync(c => c.Id == id);
        }

        public async Task<IList<TEntity>> GetDataAsync(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int? skip = null,
            int? take = null)
        {
            var query = DbSet.AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }
        public async Task<TEntity> AddWithReturn(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity;
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
