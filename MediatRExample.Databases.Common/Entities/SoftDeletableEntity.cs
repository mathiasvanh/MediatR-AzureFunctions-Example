namespace MediatRExample.Databases.Common.Entities
{
    public abstract class SoftDeletableEntity<TKey> : AuditableEntity<TKey>, ISoftDeletableEntity
    {
        public bool? IsDeleted { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
