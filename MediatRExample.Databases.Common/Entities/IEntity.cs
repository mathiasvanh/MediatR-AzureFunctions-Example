namespace MediatRExample.Databases.Common.Entities
{
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }

    public interface IEntity
    {
    }
}