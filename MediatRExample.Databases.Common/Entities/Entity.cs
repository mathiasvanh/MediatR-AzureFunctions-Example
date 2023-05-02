using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatRExample.Databases.Common.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [Key] 
        [Column(Order = 0)] 
        public virtual TKey Id { get; set; }
    }
}