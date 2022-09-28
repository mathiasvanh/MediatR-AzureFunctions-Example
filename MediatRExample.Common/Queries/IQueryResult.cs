namespace MediatRExample.Common.Queries
{
    public interface IQueryResult
    {
        bool IsSuccess { get; }

        string? FailureReason { get; }
    }
}