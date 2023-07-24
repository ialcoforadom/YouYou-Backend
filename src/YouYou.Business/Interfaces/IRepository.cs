using System.Linq.Expressions;
using YouYou.Business.Models;

namespace YouYou.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entitys);

        Task<TEntity> GetById(int id);

        Task<TEntity> GetByIdTracked(int id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Update(TEntity entity);

        Task UpdateRange(IEnumerable<TEntity> entitys);

        Task UpdateKey(TEntity t, object key);

        Task Remove(int id);

        Task RemoveRange(IEnumerable<TEntity> entitys);

        Task RemoveEntity(TEntity entity);

        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        Task<bool> CheckExist(int id);

        Task<int> SaveChanges();

        Task<TEntity> AddWithReturn(TEntity entity);
    }
}
