using System.Linq.Expressions;
using MediatRExample.Databases.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

namespace MediatRExample.Databases.Common
{
    public class BaseDbContext<TDbContext> : DbContext where TDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions<TDbContext> builderOptions) : base(builderOptions)
        {
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(true, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            ApplyAuditInfoRules();
            ApplySoftDeleteRules();
        }

        private void ApplySoftDeleteRules()
        {
            var deletedEntries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is ISoftDeletableEntity &&
                    (e.State == EntityState.Deleted));

            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Modified;
                var entity = (ISoftDeletableEntity)entry.Entity;
                entity.Deleted = DateTime.UtcNow;
                entity.IsDeleted = true;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.Created == default)
                {
                    entity.Created = DateTime.UtcNow;
                }
                else
                {
                    entity.Modified = DateTime.UtcNow;
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            ApplyConfigurations(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            ApplyQueryFilters(builder, entityTypes);

            DisableCascadeDelete(entityTypes);
        }


        // Leave these as fields, because else the filters won't work correctly.
        // If you want to change them, make a method which changes the value.
        protected bool FiltersDisabled = false;
        protected bool SoftDeleteFilterEnabled = true;

        private void ApplyQueryFilters(ModelBuilder modelBuilder, List<IMutableEntityType> entityTypes)
        {
            var clrTypes = entityTypes.Where(et => et.ClrType != null && et.BaseType == null).Select(et => et.ClrType).ToList();

            var baseFilter = (Expression<Func<IEntity, bool>>)(_ => FiltersDisabled);
            var softDeleteFilter = (Expression<Func<ISoftDeletableEntity, bool>>)(e => !SoftDeleteFilterEnabled || !(e.IsDeleted ?? false));

            foreach (var type in clrTypes)
            {
                var filters = new List<LambdaExpression>();

                if (typeof(ISoftDeletableEntity).IsAssignableFrom(type))
                {
                    filters.Add(softDeleteFilter);
                }

                var queryFilter = CombineQueryFilters(type, baseFilter, filters);
                modelBuilder.Entity(type).HasQueryFilter(queryFilter);
            }
        }

        // EFCore currently has 2 limitations:
        //
        // - In Expression<Func<TEntity, bool>>, TEntity has to be the final entity type and cannot
        //   be, for example, the interface type. To work around it, we change the type in the expression
        //   with ReplacingExpressionVisitor. See: https://github.com/aspnet/EntityFrameworkCore/issues/10257
        //
        // - Only 1 HasQueryFilter() call is supported per entity type. The last one will overwrite each call that
        //   came before it. To work around this, we combine the multiple query filters in a single expression.
        //   See: https://github.com/aspnet/EntityFrameworkCore/issues/10275
        private LambdaExpression CombineQueryFilters(Type entityType, LambdaExpression baseFilter, IEnumerable<LambdaExpression> andAlsoExpressions)
        {
            var newParam = Expression.Parameter(entityType);

            var andAlsoExprBase = (Expression<Func<IEntity, bool>>)(_ => true);
            var andAlsoExpr = ReplacingExpressionVisitor.Replace(andAlsoExprBase.Parameters.Single(), newParam, andAlsoExprBase.Body);
            foreach (var expressionBase in andAlsoExpressions)
            {
                var expression = ReplacingExpressionVisitor.Replace(expressionBase.Parameters.Single(), newParam, expressionBase.Body);
                andAlsoExpr = Expression.AndAlso(andAlsoExpr, expression);
            }

            var baseExp = ReplacingExpressionVisitor.Replace(baseFilter.Parameters.Single(), newParam, baseFilter.Body);
            var exp = Expression.OrElse(baseExp, andAlsoExpr);

            return Expression.Lambda(exp, newParam);
        }

        private static void DisableCascadeDelete(List<IMutableEntityType> entityTypes)
        {
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected void ApplyConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(TDbContext).Assembly);
        }
    }
}