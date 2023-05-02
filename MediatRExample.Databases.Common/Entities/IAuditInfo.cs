namespace MediatRExample.Databases.Common.Entities
{
    public interface IAuditInfo
    {
        DateTime? Created { get; set; }

        DateTime? Modified { get; set; }
    }
}