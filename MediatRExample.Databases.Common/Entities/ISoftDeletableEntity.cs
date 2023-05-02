using System.ComponentModel.DataAnnotations.Schema;

namespace MediatRExample.Databases.Common.Entities
{
    public interface ISoftDeletableEntity : IEntity
    {
        [Column(Order = 1000)] bool? IsDeleted { get; set; }

        [Column(Order = 1001)] DateTime? Deleted { get; set; }
    }
}
