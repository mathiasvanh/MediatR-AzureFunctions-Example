using MediatRExample.Databases.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MediatRExample.Databases.Common.Repositories
{
    public abstract class Repository<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TDbContext : DbContext
    {
        public Repository(TDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected TDbContext Context { get; set; }

        public virtual IQueryable<TEntity> All()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> AllAsNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            if (id != null)
            {
                return await DbSet.FirstOrDefaultAsync(ent => ent.Id != null && ent.Id.Equals(id));
            }

            return null;
        }

        public virtual Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return DbSet.AddAsync(entity).AsTask();
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) Context?.Dispose();
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public async Task AddRangeAsync(IList<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public async Task UpdateRangeAsync(IList<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }
    }
}