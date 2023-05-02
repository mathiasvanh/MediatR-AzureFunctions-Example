using Microsoft.EntityFrameworkCore;

namespace MediatRExample.Databases.Common.Entities
{
    [Keyless]
    public abstract class KeylessEntity : IEntity
    {
       
    }
}