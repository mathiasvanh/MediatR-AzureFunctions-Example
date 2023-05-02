using Microsoft.EntityFrameworkCore;

namespace MediatRExample.Databases.Common.Repositories
{
    public abstract class KeylessEntityRepository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        public KeylessEntityRepository(TDbContext context)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) Context?.Dispose();
        }
    }
}