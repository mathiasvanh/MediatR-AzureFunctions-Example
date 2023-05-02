using MediatRExample.Databases.Common.Entities;

namespace MediatRExample.Databases.Common.Repositories
{
    public interface IDeletableEntityRepository<TEntity, in TKey> : IRepository<TEntity, TKey>
        where TEntity : class, ISoftDeletableEntity, IEntity<TKey>
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}