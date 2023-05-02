using MediatRExample.Databases.Common.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MediatRExample.Databases.Common.Repositories
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
    {
        IQueryable<TEntity> All();
        
        IQueryable<TEntity> AllAsNoTracking();

        Task<TEntity?> GetByIdAsync(TKey id);

        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        Task AddRangeAsync(IList<TEntity> entities);

        void Update(TEntity entity);

        Task UpdateRangeAsync(IList<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteRange(IList<TEntity> entities);

        Task<int> SaveChangesAsync();
    }
    
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();
    }
}