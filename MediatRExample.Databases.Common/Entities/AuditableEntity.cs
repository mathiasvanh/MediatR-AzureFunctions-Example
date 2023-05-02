namespace MediatRExample.Databases.Common.Entities
{
    public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditInfo
    {
        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}